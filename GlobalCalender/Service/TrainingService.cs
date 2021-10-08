using GlobalCalender.Models;
using GlobalCalender.Models.ViewModel;
using GlobalCalender.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Service
{
    public class TrainingService : ITrainingService
    {
        private readonly ApplicationDbContext _db;

        public TrainingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(TrainingVM model)
        {
            var startDate = DateTime.ParseExact(model.StartDate, "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None);;
            //var startDate = DateTime.Parse(model.StartDate);
            //var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            var endDate = DateTime.ParseExact(model.StartDate, "M/d/yyyy h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None).AddMinutes(Convert.ToDouble(model.Duration));
            //var user = _db.Users.FirstOrDefault(u => u.Id == model.UserId);
            var trainer = _db.Users.FirstOrDefault(u => u.Id == model.TrainerId);
            if (model != null && model.Id > 0)
            {
                //update
                var scheduletraining = _db.ScheduleTrainings.FirstOrDefault(x => x.Id == model.Id);
                scheduletraining.CourseName = model.CourseName;
                scheduletraining.PreRequisite = model.PreRequisite;
                scheduletraining.StartDate = startDate;
                scheduletraining.EndDate = endDate;
                scheduletraining.Duration = model.Duration;
                //scheduletraining.TeamsLink = model.TeamsLink;
                scheduletraining.TrainerId = model.TrainerId;
                //scheduletraining.UserId = model.UserId;
                scheduletraining.IsTrainerApproved = false;
                scheduletraining.AdminId = model.AdminId;
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                //create
                Training scheduletraining = new Training()
                {
                    CourseName = model.CourseName,
                    PreRequisite = model.PreRequisite,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    //TeamsLink = model.TeamsLink,
                    TrainerId = model.TrainerId,
                    //UserId = model.UserId,
                    IsTrainerApproved = false,
                    AdminId = model.AdminId
                };
                _db.ScheduleTrainings.Add(scheduletraining);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

        public async Task<int> ConfirmEvent(int id)
        {
            var scheduletraining = _db.ScheduleTrainings.FirstOrDefault(x => x.Id == id);
            if (scheduletraining != null)
            {
                scheduletraining.IsTrainerApproved = true;
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var scheduletraining = _db.ScheduleTrainings.FirstOrDefault(x => x.Id == id);
            if (scheduletraining != null)
            {
                _db.ScheduleTrainings.Remove(scheduletraining);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public TrainingVM GetById(int id)
        {
            return _db.ScheduleTrainings.Where(x => x.Id == id).ToList().Select(c => new TrainingVM()
            {
                Id = c.Id,
                CourseName = c.CourseName,
                PreRequisite = c.PreRequisite,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = c.Duration,
                //TeamsLink = c.TeamsLink,
                IsTrainerApproved = c.IsTrainerApproved,
                //UserId = c.UserId,
                TrainerId = c.TrainerId,
                //UserName = _db.Users.Where(x => x.Id == c.UserId).Select(x => x.Name).FirstOrDefault(),
                TrainerName = _db.Users.Where(x => x.Id == c.TrainerId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public List<TrainerVM> GetTrainerList()
        {
            var Trainers = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x=>x.Name==Helper.Trainer) on userRoles.RoleId equals roles.Id
                            select new TrainerVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return Trainers;
        }

        public List<UserVM> GetUserList()
        {
            var Users = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.User) on userRoles.RoleId equals roles.Id
                            select new UserVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();

            return Users;
        }

        public List<TrainingVM> TrainersEventsById(string trainerId)
        {
            return _db.ScheduleTrainings.Where(x => x.TrainerId == trainerId).ToList().Select(c => new TrainingVM()
            {
                Id = c.Id,
                CourseName = c.CourseName,
                PreRequisite = c.PreRequisite,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = c.Duration,
                //TeamsLink = c.TeamsLink,
                IsTrainerApproved = c.IsTrainerApproved
            }).ToList();
        }

        public List<TrainingVM> UsersEventsById(string userId)
        {
            return _db.ScheduleTrainings.Select(c => new TrainingVM()
            {
                Id = c.Id,
                CourseName = c.CourseName,
                PreRequisite = c.PreRequisite,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = c.Duration,
                //TeamsLink = c.TeamsLink,
                IsTrainerApproved = c.IsTrainerApproved
            }).ToList();
        }
    }
}
