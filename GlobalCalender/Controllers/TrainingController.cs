using GlobalCalender.Service;
using GlobalCalender.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public IActionResult Index()
        {
            ViewBag.TrainerList = _trainingService.GetTrainerList();
            ViewBag.UserList = _trainingService.GetUserList();
            ViewBag.Duration = Helper.GetTimeDropDown();
            return View();
        }
    }
}
