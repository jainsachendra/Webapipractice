using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsersDataAcess;
namespace UserService
{
    public class UserSecurity
    {
        public static bool login(string uname,string Lname)
        {
            using (tamedEntities obj=new tamedEntities())
            {
                return obj.Users.Any(user => user.Fname.Equals(uname, StringComparison.OrdinalIgnoreCase)&& user.Lname==Lname);
            }

        }
    }
}