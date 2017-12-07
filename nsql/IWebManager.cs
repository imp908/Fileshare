﻿using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IWebManagers
{

      /// <summary>
    /// Gets response from URL with method
    /// </summary>
    public interface IWebManager
    {
        void SetCredentials(NetworkCredential credentials);
        WebResponse GetResponse(string method);
        WebResponse GetResponse64(string method);
        WebRequest AddRequest(string url);
        
   }
   

    /// <summary>
    /// Reads response and converts it to string
    /// </summary>
    public interface IResponseReader
    {
        string ReadResponse(HttpWebResponse response);
        string ReadResponse(WebResponse response);
        string ReadResponse(HttpResponseMessage response);
        string ReadResponse(IHttpActionResult response);
   }

}