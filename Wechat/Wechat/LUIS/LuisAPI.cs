using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wechat.Model
{
    public class LuisAPI
    {
        /// <summary>
        /// 查询LUIS意图对应的回答
        /// </summary>
        /// <param name="query">请求的文本</param>
        /// <returns></returns>
        public static string PostMessage(string query)
        {
            LuisMessage luisMsg = LuisAPI.Default.GetLuis(query);
            if (luisMsg == null || luisMsg.entities == null || luisMsg.entities.Count <= 0)
            {
                return "好尴尬啊";
            }
            string reponse = luisMsg.topScoringIntent.intent;
            return reponse;
        }

        /// <summary>
        /// 
        /// </summary>
        public static LuisAPI Default
        {
            get { return new LuisAPI(); }
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        private static readonly string URI = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/13526516-0de0-4ef4-a617-4ab3ae6b9c75?subscription-key=ed92b9e259df4b2ab49e1b976ad0a739&verbose=true&timezoneOffset=0&q={0}";

        /// <summary>
        /// 请求LUIS
        /// </summary>
        /// <param name="Query">请求内容</param>
        /// <returns>LUIS返回的对象</returns>
        public LuisMessage GetLuis(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            LuisMessage Data = new LuisMessage();
            using (HttpClient client = new HttpClient())
            {
                string RequestURI = string.Format(URI, Query);
                var task = client.GetAsync(RequestURI);
                HttpResponseMessage msg = task.Result;
                if (msg.IsSuccessStatusCode)
                {
                    var task1 = msg.Content.ReadAsStringAsync();
                    var JsonDataResponse = task1.Result;
                    Data = JsonConvert.DeserializeObject<LuisMessage>(JsonDataResponse);
                }
            }
            return Data;
        }
    }
}
