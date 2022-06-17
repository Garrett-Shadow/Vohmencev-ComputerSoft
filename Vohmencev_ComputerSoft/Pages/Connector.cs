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
        private static Database.Vohmencev_ComputerSoftEntities DatabaseConnector;

        public static Database.Vohmencev_ComputerSoftEntities GetModel()
        {
            if (DatabaseConnector == null)
            {
                DatabaseConnector = new Database.Vohmencev_ComputerSoftEntities();
            }

            return DatabaseConnector;
        }

        public static Database.Staff GetMyProfile()
        {
            return StaffProfile;
        }

        public static bool Authorize(string login, string password)
        {
            string GetLogin = login.Trim();
            string GetPassword = password.Trim();
            StaffProfile = GetModel().Staff.Where(Employee => Employee.StaffLogin == GetLogin && Employee.StaffPassword == GetPassword).FirstOrDefault();
            return StaffProfile != null;
        }
    }
}
