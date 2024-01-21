using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebsite.Models
{
    public interface ILoginServices
    {
        LoginModel LoginSubmit(LoginModel currentUser);
        RegisterModel SignUpSubmit(RegisterModel newUser);

    }
}
