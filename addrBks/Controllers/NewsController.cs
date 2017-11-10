﻿using NewsAPI.Extensions;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Http;

using static NewsAPI.Helpers.OrientNewsHelper;

namespace NewsAPI.Controllers
{
    public class NewsController : ApiController
    {
        private readonly INews news;
        private readonly IAddressBookProxy proxy;
        private readonly IJsonValidator jsonValidator;
        private readonly IUserAuthenticator userAuthenticator;

        public NewsController(INews news, IAddressBookProxy proxy, IJsonValidator jsonValidator, IUserAuthenticator userAuthenticator)
        {
            this.news = news;
            this.proxy = proxy;
            this.jsonValidator = jsonValidator;
            this.userAuthenticator = userAuthenticator;
        }


        [HttpPost]
        public IHttpActionResult POST([FromBody]JObject json1)
        {
            string json = string.Join("", Regex.Split(json1.ToString(), @"(?:\r\n|\n|\r)"));

            if (jsonValidator.Validate(json))
            {
                // Получаем хелпер
                var newsHelper = new OrientNewsHelper();

                // Осуществляем авторизацию в OrientDb
                newsHelper.Authorize();

                // Создание объекта
                var response = news.CreateEntity(json);

                // Получение id созданного объекта
                var createdEntityId = proxy.ReturnAddedEntityId(response).ExtractEntityId(ConfigurationManager.AppSettings["orient_id_name"]);

                // Получение пользователя из реквеста
                var name = userAuthenticator.AuthenticateUser(base.User);

                // Создание edge авторства
                news.CreateAuthorEdge(name, createdEntityId).ExecuteAsync(new CancellationToken());

                // возврат id созданного объекта
                return new TextResult(createdEntityId, Request);
            }
            else { throw new Exception("invalid json string"); }
        }


        [HttpPost]
        public IHttpActionResult POST(int id, [FromBody]string json)
        {
            if (jsonValidator.Validate(json))
            {
                // Получаем хелпер
                var newsHelper = new OrientNewsHelper();

                // Осуществляем авторизацию в OrientDb
                newsHelper.Authorize();

                // Создание объекта
                var response = news.CreateEntity(json);

                // Получение id созданного объекта
                var createdEntityId = proxy.ReturnAddedEntityId(response).ExtractEntityId(ConfigurationManager.AppSettings["orient_id_name"]);

                // Получение пользователя из реквеста
                var name = userAuthenticator.AuthenticateUser(base.User);

                // Создание edge авторства
                news.CreateAuthorEdge(name, createdEntityId).ExecuteAsync(new CancellationToken());

                // Создание edge comment
                news.CreateCommentEdge(id.ToString(), createdEntityId).ExecuteAsync(new CancellationToken());

                // возврат id созданного объекта
                return new TextResult(createdEntityId, Request);
            }
            else { throw new Exception("invalid json string"); }
        }

        public IHttpActionResult PUT(int id, [FromBody]string json_changes)
        {
            if (jsonValidator.Validate(json_changes))
            {
                // Получаем хелпер
                var newsHelper = new OrientNewsHelper();

                // Осуществляем авторизацию в OrientDb
                newsHelper.Authorize();

                // Получение объекта по его Id
                var helper = new OrientNewsHelper();
                string target_entity = helper.GetEntity(id.ToString());

                // вычисление изменений
                var jsonToPut = helper.CreateChangesJson(target_entity, json_changes);

                // применение изменений
                var response = news.UpdateEntity(id.ToString(), jsonToPut);

                // возврат значения, который возвращает OrientDB
                return response;
            }
            throw new NotImplementedException();
        }

        public IHttpActionResult Delete()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        //[Route("api/News/{type}")]
        public IHttpActionResult Get(string type)
        {
            // Получаем хелпер
            var newsHelper = new OrientNewsHelper();

            // Осуществляем авторизацию в OrientDb
            newsHelper.Authorize();

            // Получаем запрошенную сущность (новость, блог, коммент или проч.)
            string requestedEntity = newsHelper.GetEntitiesOfType(type);

            // Готовим response к проксированию, т.к. требуется применить fetchPlan и извлечь нужный json с контентом запрошенной сущности
            var response = new TextResult(requestedEntity, Request);

            // возврат проскированного контента запрошенной сущности
            return proxy.ReturnRequestedEntityTraverseWithFetchPlan(response);
        }

        [HttpGet]
        //[Route("api/News/{type}/{count}")]
        public IHttpActionResult Get(string type, int count)
        {
            // Получаем хелпер
            var newsHelper = new OrientNewsHelper();

            // Осуществляем авторизацию в OrientDb
            newsHelper.Authorize();

            // Получаем запрошенную сущность (новость, блог, коммент или проч.)
            string requestedEntity = newsHelper.GetEntitiesOfType(type, count);

            // Готовим response к проксированию, т.к. требуется применить fetchPlan и извлечь нужный json с контентом запрошенной сущности
            var response = new TextResult(requestedEntity, Request);

            // возврат проскированного контента запрошенной сущности
            return proxy.ReturnRequestedEntityTraverseWithFetchPlan(response);
        }

        [HttpGet]
        //[Route("api/News/{type}/{start}/{count}")]
        public IHttpActionResult Get(string type, int start, int count)
        {
            // Получаем хелпер
            var newsHelper = new OrientNewsHelper();

            // Осуществляем авторизацию в OrientDb
            newsHelper.Authorize();

            // Получаем запрошенную сущность (новость, блог, коммент или проч.)
            string requestedEntity = newsHelper.GetEntitiesOfTypeInSpecificBounds(type, start, count);

            // Готовим response к проксированию, т.к. требуется применить fetchPlan и извлечь нужный json с контентом запрошенной сущности
            var response = new TextResult(requestedEntity, Request);

            // возврат проскированного контента запрошенной сущности
            return proxy.ReturnRequestedEntityTraverseWithFetchPlan(response);
        }

    }
}
