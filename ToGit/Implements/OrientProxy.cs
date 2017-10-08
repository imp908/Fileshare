﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NewsAPI.Interfaces;
using NewsAPI.Helpers;
using System.Configuration;

namespace NewsAPI.Implements
{
    public class OrientProxy : IAddressBookProxy
    {
        public IHttpActionResult ReturnAddedEntityId(IHttpActionResult ar)
        {
            return new OrientNewsHelper.ReturnAddedEntityIdWithExecute(ar,
                ConfigurationManager.AppSettings["orient_id_name"]
                );
        }

        public IHttpActionResult ReturnPersonInfo(IHttpActionResult ar)
        {
            return new OrientNewsHelper.ReturnPersonInfo(ar);                
        }
      
    }
}