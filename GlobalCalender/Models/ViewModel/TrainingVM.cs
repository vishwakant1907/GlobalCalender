using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Models.ViewModel
{
    public class TrainingVM
    {
        public int? Id { get; set; }
        public string CourseName { get; set; }
        public string PreRequisite { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        //public string TeamsLink { get; set; }
        public string TrainerId { get; set; }
        public string UserId { get; set; }
        public bool IsTrainerApproved { get; set; }
        public string AdminId { get; set; }

        public string TrainerName { get; set; }
        public string UserName { get; set; }
        public string AdminName { get; set; }
        public bool IsForUser { get; set; }
    }
}
