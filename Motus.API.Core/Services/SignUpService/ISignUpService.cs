using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motus.API.Core.Services.SignUpService
{
    public interface ISignUpService
    {
        public bool AddUser(string firstname, string lastname, string email, DateTime data, string phonenumber, string password);
        public bool DeleteUser(string email);
        public bool login(string email, string password);
        public void UserActivation(string email, string token);
    }
}
