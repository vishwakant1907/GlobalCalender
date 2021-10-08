using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Models
{
    public class Training
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string PreRequisite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        //public string TeamsLink { get; set; }
        public string TrainerId { get; set; }
        public string UserId { get; set; }
        public bool IsTrainerApproved { get; set; }
        public string AdminId { get; set; }
    }
}
