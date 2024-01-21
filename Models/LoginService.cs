using Microsoft.Ajax.Utilities;
using MVCWebsite.Desgin_Parttern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebsite.Models
{
    public class LoginService: ILoginServices
    {
        private IBookEntities bookEntities;
        public LoginService(IBookEntities _bookEntities)
        {
            this.bookEntities = _bookEntities;
        }

        public LoginModel LoginSubmit(LoginModel currentUser)
        {
            try
            {
                var query = bookEntities.UserInfor
                    .FirstOrDefault(x => x.username == currentUser.username && x.password == currentUser.password);
                LoginModel model = new LoginModel
                {
                    username = query.username,
                    password = query.password,
                    ifValid = true
                    
                };

                return model;
            }
            catch 
            {
                return new LoginModel {ifValid = false };
            }
        }

        public RegisterModel SignUpSubmit(RegisterModel newUser)
        {
            try
            {
                var query = bookEntities.UserInfor.FirstOrDefault(x => x.username == newUser.username);
                if (query != null)
                {
                    return new RegisterModel { ifValid = false, ifMatch = true };
                }

                if (newUser.password != newUser.retypePassword)
                {
                    return new RegisterModel
                    {
                        ifMatch = false,
                        ifValid = true
                    };
                }

                bookEntities.UserInfor.Add(
                    new UserInfor
                    {
                        password = newUser.password,
                        username = newUser.username,
                    });
                bookEntities.SaveChanges();

                var user = bookEntities.UserInfor.FirstOrDefault(x => x.username == newUser.username);
                bookEntities.UserRolesMapping.Add(
                    new UserRolesMapping
                    {
                        RoleID = 2,
                        UserID = user.userId
                    }

                    );

                bookEntities.SaveChanges();
                return new RegisterModel { ifValid = true, ifMatch = true };

            }
            catch
            {              
                return new RegisterModel { ifValid = true, ifMatch = false};
            }
            
        }

    }
}