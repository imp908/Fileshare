﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Linq;

using IJsonManagers;

namespace JsonManagers
{
  
    /// <summary>
    /// JSON manager revised 
    /// Newtonsoft JSON wrapper
    /// extracts IJEnumerable<Jtoken> from string
    /// deserializess to collection of objects or strings (if convertable)
    /// converts to string results with options
    /// </summary>
    public class JSONManager : IJsonManger
    {

        public IJEnumerable<JToken> ExtractFromParentNode(string input, string parentNodeName)
        {
            IJEnumerable<JToken> result = null;
            result = JToken.Parse(input)[parentNodeName];
            return result;
        }
        public IJEnumerable<JToken> ExtractFromParentChildren(string input,  string childNodeName)
        {
            IJEnumerable<JToken> result = null;
            result = JToken.Parse(input).Children()[childNodeName];
            return result;
        }
        public IJEnumerable<JToken> ExtractFromParentChildNode(string input, string parentNodeName, string childNodeName)
        {
            IJEnumerable<JToken> result = null;
            result = JToken.Parse(input)[parentNodeName].Children()[childNodeName];
            return result;
        }
        public IJEnumerable<JToken> ExtractFromChildNode(string input, string childNodeName)
        {
            IJEnumerable<JToken> result = null;
            result = JToken.Parse(input).Children()[childNodeName];
            return result;
        }
       
        public IJEnumerable<JToken> ExtractFromParentNode(string input)
        {
            IJEnumerable<JToken> result = null;
            result = JToken.Parse(input);
            return result;
        }

        public IEnumerable<T> DeserializeFromParentNode<T>(string input, string parentNodeName) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollection<T>(ExtractFromParentNode(input, parentNodeName));
            return result;
        }
        public IEnumerable<T> DeserializeFromParentChildren<T>(string input, string childNodeName) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollectionObjColl<T>(ExtractFromParentChildren(input, childNodeName));
            return result;
        }
        public IEnumerable<T> DeserializeFromParentChildNode<T>(string input, string parentNodeName, string childNodeName) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollection<T>(ExtractFromParentChildNode(input, parentNodeName, childNodeName));
            return result;
        }
        public IEnumerable<T> DeserializeFromChildNode<T>(string input, string childNodeName) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollection<T>(ExtractFromChildNode(input, childNodeName));
            return result;
        }

        public IEnumerable<T> DeserializeFromParentNode<T>(string input) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollection<T>(ExtractFromParentNode(input));
            return result;
        }
        public IEnumerable<T> DeserializeFromParentNodeObjColl<T>(string input) where T : class
        {
            IEnumerable<T> result = null;
            result = JTokensToCollectionObjColl<T>(ExtractFromParentNode(input));
            return result;
        }      
        
        public string SerializeObject(object input_, JsonSerializerSettings settings_=null)
        {
            string result = string.Empty;          
            result = JsonConvert.SerializeObject(input_, settings_);
            return result;
        }
        public string SerializeObject(object input_, JsonConverter converter_ = null)
        {
            string result = string.Empty;
            result = JsonConvert.SerializeObject(input_, converter_);
            return result;
        }
        public string SerializeObject(object input_)
        {
            JsonSerializerSettings settings_ = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                DateFormatString = @"yyyy-MM-dd HH:mm:ss"
            };

            string result = string.Empty;
            result = JsonConvert.SerializeObject(input_, settings_);
            if (result == @"{}") { result = null; }
            return result;
        }


        public IEnumerable<T> JTokensToCollection<T>(IEnumerable<JToken> input) where T : class
        {
            IEnumerable<T> result = null;
            result = (from s in input select s.ToObject<T>()).ToList();
            return result;
        }
        public IEnumerable<T> JTokensToCollectionObjColl<T>(IEnumerable<JToken> input) where T : class
        {
            IEnumerable<T> result = null;
            foreach (var a in input)
            {
                result = a.ToObject<List<T>>();
            }
            return result;
        }
        public T JTokensToCollectionObj<T>(IEnumerable<JToken> input) where T : class
        {
            T result = null;
            foreach (var a in input)
            {
                result = a.ToObject<T>();
            }
            return result;
        }


        public List<string> JTokenToCollection(IEnumerable<JToken> input)
        {
            List<string> result = new List<string>();
            foreach (JToken jt in input.Children())
            {
                result.Add(JsonConvert.SerializeObject(jt));
            }
            return result;
        }
        public string CollectionToStringFormat<T>(IEnumerable<T> list_, JsonSerializerSettings jss = null) where T : class
        {
            string result = null;
            result = JsonConvert.SerializeObject(list_, jss);
            return result;
        }

    }

    public class BracketsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString().Replace("[", "").Replace("]",""));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            if(objectType == typeof(string)) { return true; }
            throw new NotImplementedException();
        }
    }

}