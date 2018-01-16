﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NewsAPI.Interfaces
{
    public interface IAddressBookProxy
    {
        IHttpActionResult ReturnAddedEntityId(IHttpActionResult ar);
        IHttpActionResult ReturnRequestedEntityWithFetchPlan(IHttpActionResult ar);
        IHttpActionResult ReturnRequestedEntityTraverseWithFetchPlan(IHttpActionResult ar);
        IHttpActionResult ReturnPersonInfo(IHttpActionResult ar);
        IHttpActionResult ReturnPersonGuid(IHttpActionResult ar);
        
    }
}