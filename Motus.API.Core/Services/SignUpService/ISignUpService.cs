﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motus.API.Core.Services.SignUpService
{
    public interface ISignUpService
    {
        public void AddUser(string firstname, string lastname, string email, DateTime data, string phonenumber, string password);

        public void DeleteUser(string email);

        public void UserActivation(string email);
    }
}
