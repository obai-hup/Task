using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Data
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session , string key , object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJosn<T>(this ISession session, string key)
        {
            var SessionData = session.GetString(key);
            return SessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(SessionData);
        }
    }
}
