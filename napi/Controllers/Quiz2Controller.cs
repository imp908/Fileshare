﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Web.Http.Cors;

namespace NewsAPI.Controllers
{
    public class Quiz2Controller : ApiController
    {
        OrientRealization.IOrientRepo repo_;
        Quizes.QuizNewUOW quizUow;
        
        public Quiz2Controller(){
            string host= string.Format("{0}:{1}"
            ,ConfigurationManager.AppSettings["OrientTestHost"],ConfigurationManager.AppSettings["OrientPort"]);

            repo_= OrientRealization.RepoFactory.NewOrientRepo(ConfigurationManager.AppSettings["OrientDevDB"]
            ,host
            ,ConfigurationManager.AppSettings["orient_dev_login"]
            ,ConfigurationManager.AppSettings["orient_dev_pswd"]);

            quizUow = new Quizes.QuizNewUOW(repo_);
            //quizUow.InitClasses();
        }

        
        [HttpGet]
        [Route("api/Quiz2/Gen")]
        public IHttpActionResult Gen(){
            WebManagers.ReturnEntities response = null;
            string result = string.Empty;
            quizUow.QuizGenerate();
            response=new WebManagers.ReturnEntities(result, Request);
        return response;
        }
        [HttpGet]
        [Route("api/Quiz2/Init")]
        public IHttpActionResult Initialize(){
            WebManagers.ReturnEntities response = null;
            string result = string.Empty;
            quizUow.InitClasses();
            response=new WebManagers.ReturnEntities(result, Request);
        return response;
        }

        [HttpGet]
        [Route("api/Quiz2")]
        public IHttpActionResult Get()
        {
            WebManagers.ReturnEntities response = null;
            string result = string.Empty;
            result=quizUow.QuizGetStr();
            response=new WebManagers.ReturnEntities(result, Request);
            return response;
        }

        [HttpPost]
        [Route("api/Quiz2")]
        public IHttpActionResult Post(IEnumerable<POCO.QuizNewGet> qz_)
        {
            WebManagers.ReturnEntities response = null;
            string result = string.Empty;
            try{
              quizUow.QuizPost(qz_);
            }catch(Exception e){
              result = e.Message;
            }
            response=new WebManagers.ReturnEntities(result, Request);
            return response;
        }

        [HttpPost]
        [Route("api/Quiz2/Delete")]
        public IHttpActionResult Delete(IEnumerable<POCO.QuizNewGet> qz_)
        {
            WebManagers.ReturnEntities response = null;
            string result = string.Empty;            
            quizUow.QuizDelete(qz_);
            response=new WebManagers.ReturnEntities(result, Request);
            return response;
        }


    }
}
