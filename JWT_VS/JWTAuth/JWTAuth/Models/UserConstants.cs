using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuth.Models
{
    public class UserConstants
    {
        public static List<UserModel> lstUsers = new List<UserModel>()
        { 
           new UserModel() { UserName="sachin" ,EmailAddress="sach@gmail.com" ,GivenName="" ,Surname="Shetty" ,Role="VP", Password="Shetty1608"},

           new UserModel() {UserName="sai" ,EmailAddress="sai@gmail.com", GivenName="Sai" ,Surname="Shetty" ,Role="AVP", Password="Shetty1905" }
        };
    }
}
