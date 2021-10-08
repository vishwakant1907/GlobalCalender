using GlobalCalender.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Service
{
   public interface ITrainingService
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
