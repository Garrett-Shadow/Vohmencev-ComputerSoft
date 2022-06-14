using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vohmencev_ComputerSoft.Pages
{
    public static class Connector
    {
        private static Database.Staff StaffProfile;
        private static Database.ComputerSoftEntities DatabaseConnector;

        public static Database.ComputerSoftEntities GetModel()
        {
            if (DatabaseConnector == null)
            {
                DatabaseConnector = new Database.ComputerSoftEntities();
            }

            return DatabaseConnector;
        }

        public static Database.Staff GetMyProfile()
        {
            return StaffProfile;
        }

        public static bool Authorize(string login, string password)
        {
            string getlogin = login.Trim();
            string getpassword = password.Trim();
            StaffProfile = GetModel().Staff.Where(Employee => Employee.StaffLogin == getlogin && Employee.StaffPassword == getpassword).FirstOrDefault();
            return StaffProfile != null;
        }
    }
}
