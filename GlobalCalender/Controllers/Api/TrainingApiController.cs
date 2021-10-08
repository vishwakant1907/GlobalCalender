using GlobalCalender.Models.ViewModel;
using GlobalCalender.Service;
using GlobalCalender.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GlobalCalender.Controllers.Api
{
    [Route("api/Training")]
    [ApiController]
    public class TrainingApiController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        public TrainingApiController(ITrainingService trainingService, IHttpContextAccessor httpContextAccessor)
        {
            _trainingService = trainingService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        }
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(TrainingVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                //HttpClient//

                commonResponse.status =  
                
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string trainerId)
        {
            CommonResponse<List<TrainingVM>> commonResponse = new CommonResponse<List<TrainingVM>>();
            try
            {


                //httpclient res
               // commonResponse=Response of httpclient
                //if (role == Helper.User)
                //{
                //    commonResponse.dataenum = _trainingService.UsersEventsById(loginUserId);
                //    commonResponse.status = Helper.success_code;
                //}
                //else if (role == Helper.Trainer)
                //{
                //    commonResponse.dataenum = _trainingService.TrainersEventsById(loginUserId);
                //    commonResponse.status = Helper.success_code;
                //}
                //else
                //{
                //    commonResponse.dataenum = _trainingService.TrainersEventsById(trainerId);
                //    commonResponse.status = Helper.success_code;
                //}
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<TrainingVM> commonResponse = new CommonResponse<TrainingVM>();
            try
            {

                commonResponse.dataenum = _trainingService.GetById(id);
                commonResponse.status = Helper.success_code;

            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteAppoinment/{id}")]
        public async Task<IActionResult> DeleteAppoinment(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = await _trainingService.Delete(id);
                commonResponse.message = commonResponse.status == 1 ? Helper.trainingDeleted : Helper.somethingWentWrong;

            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmEvent/{id}")]
        public IActionResult ConfirmEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = _trainingService.ConfirmEvent(id).Result;
                if (result > 0)
                {
                    commonResponse.status = Helper.success_code;
                    commonResponse.message = Helper.trainingConfirm;
                }
                else
                {

                    commonResponse.status = Helper.failure_code;
                    commonResponse.message = Helper.trainingConfirmError;
                }

            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

    }
}
