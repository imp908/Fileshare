﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NewsAPI.Interfaces
{
    public interface IUserSettings
    {
        IHttpActionResult GetUserSettings(string userLogin);
        IHttpActionResult PostUserSettings(string userLogin, string json);
    }
}