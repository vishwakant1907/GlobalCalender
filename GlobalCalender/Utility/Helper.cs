using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCalender.Utility
{
    public  static class Helper
    {
        public static string Admin = "Admin";
        public static string Trainer = "Trainer";
        public static string User = "User";
        public static string trainingAdded = "Training added successfully.";
        public static string trainingUpdated = "Training updated successfully.";

        public static string trainingDeleted = "Training deleted successfully.";
        public static string trainingExists = "Training for selected date and time already exists.";
        public static string trainingNotExists = "Training not exists.";
        public static string trainingConfirm = "Training confirm successfully.";
        public static string trainingConfirmError = "Error while confirming training.";
        public static string trainingtAddError = "Something went wront, Please try again.";
        public static string trainingUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";

        public static int success_code = 1;
        public static int failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Admin,Text=Helper.Admin}
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Trainer,Text=Helper.Trainer},
                    new SelectListItem{Value=Helper.User,Text=Helper.User}
                };
            }

            //return new List<SelectListItem>
            //    {
            //        new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
            //        new SelectListItem{Value=Helper.Trainer,Text=Helper.Trainer},
            //        new SelectListItem{Value=Helper.User,Text=Helper.User}
            //    };
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 8; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
