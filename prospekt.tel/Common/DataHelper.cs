using prospekt.tel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prospekt.tel.Common
{
    public class DataHelper
    {
        public static string GetUserRealName(string uName)
        {
            ApplicationDbContext adb = new ApplicationDbContext();
            //psEnt db = new psEnt();
            var udl = adb.Users.Where(x => x.UserName == uName).FirstOrDefault();
            var res = udl.userFam + " " + udl.userIm.Substring(0, 1) + "." + udl.userOt.Substring(0, 1) + ".";
            return res;
        }
    }
}