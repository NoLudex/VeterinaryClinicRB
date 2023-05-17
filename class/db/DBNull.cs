using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace VeterinaryClinicRB
{
    public class DBNull
    {
        public static bool CheckIfDoctorHasAccounts(string doctorID)
        {
            XmlDocument accountDoc = new XmlDocument();
            accountDoc.Load("./database/account.xml");
            XmlNodeList users = accountDoc.SelectNodes("//User");

            foreach (XmlNode user in users)
                if (user.Attributes["doctor-id"].Value == doctorID)
                    return true;
            
            return false;
        }
    }
}