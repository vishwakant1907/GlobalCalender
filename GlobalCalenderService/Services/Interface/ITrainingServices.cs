using GlobalCalenderService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalenderService.Services.Interface
{
  public  interface ITrainingServices
    {
        public List<TrainerVM> GetTrainerList();
        public List<UserVM> GetUserList();
        public Task<int> AddUpdate(TrainingVM model);
        public List<TrainingVM> TrainersEventsById(string trainerId);
        public List<TrainingVM> UsersEventsById(string userId);
        public TrainingVM GetById(int id);
        public Task<int> Delete(int id);
        public Task<int> ConfirmEvent(int id);
    }
}
