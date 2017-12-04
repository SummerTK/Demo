﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Wechat.Model
{
    public class MSBot
    {
        public async static Task<string> PostMessage(string message)
        {
            HttpClient client;
            HttpResponseMessage response;

            bool IsReplyReceived = false;

            string ReceivedString = null;

            client = new HttpClient();
            client.BaseAddress = new Uri("https://directline.botframework.com/api/conversations/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BotConnector", "1-o_-ID2Nd4.cwA.sWU.B6_lTPHYhWgwUoCb_7lXXIxkuq4xs5a5TLqh436AsDU");
            // for leon
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BotConnector", "McfcUTJYJTo.cwA.mfY.nuvLSsBQYETBygIfHUCIIHOpBz-LnAUWEGf-QMOFxR8");
            // for doudou
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BotConnector", "iCoMHD3whk8.cwA.50s.vlUvoUn7IjjRuHkSeNZQ2dOIHHTOftz07bAJRF2WJy8");


            response = await client.GetAsync("/api/tokens/");
            if (response.IsSuccessStatusCode)
            {
                var conversation = new Conversation();
                StringContent content = new StringContent(JsonConvert.SerializeObject(conversation));
                response = await client.PostAsync("/api/conversations/", content);
                //response = await client.PostAsync("/api/conversations/", null);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Conversation ConversationInfo = JsonConvert.DeserializeObject<Conversation>(json);
                    //Conversation ConversationInfo = response.Content.ReadAsAsync(typeof(Conversation)).Result as Conversation;
                    string conversationUrl = ConversationInfo.conversationId + "/messages/";
                    Message msg = new Message() { text = message };
                    StringContent cont = new StringContent(JsonConvert.SerializeObject(msg),Encoding.UTF8,"application/json");
                    response = await client.PostAsync(conversationUrl, cont);
                    if (response.IsSuccessStatusCode)
                    {
                        response = await client.GetAsync(conversationUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string str = await response.Content.ReadAsStringAsync();

                            MessageSet BotMessage = JsonConvert.DeserializeObject<MessageSet>(str);
                            ReceivedString = BotMessage.messages[1].text;
                            IsReplyReceived = true;
                        }
                    }
                }

            }
            return ReceivedString;
        }

        //public async static Task<string> PostMessage(string message)
        //{
        //    HttpClient client;
        //    HttpResponseMessage response;
        //    bool IsReplyReceived = false;
        //    string ReceivedString = null;
        //    client = new HttpClient();
        //    client.BaseAddress = new Uri("https://directline.botframework.com/api/conversations/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    ///bot中
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BotConnector", "McfcUTJYJTo.cwA.mfY.nuvLSsBQYETBygIfHUCIIHOpBz-LnAUWEGf-QMOFxR8");
        //    response = await client.GetAsync("/api/tokens/");
        //    ReceivedString = response.IsSuccessStatusCode.ToString();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var conversation = new Conversation();
        //        response = await client.PostAsJsonAsync("/api/conversations/", conversation);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Conversation ConversationInfo = response.Content.ReadAsAsync(typeof(Conversation)).Result as Conversation;
        //            string conversationUrl = ConversationInfo.conversationId + "/messages/";
        //            Message msg = new Message() { text = message };
        //            response = await client.PostAsJsonAsync(conversationUrl, msg);
        //            ReceivedString = "这里有问题3";
        //            if (response.IsSuccessStatusCode)
        //            {
        //                response = await client.GetAsync(conversationUrl);
        //                ReceivedString = "这里有问题4";
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    MessageSet BotMessage = response.Content.ReadAsAsync(typeof(MessageSet)).Result as MessageSet;
        //                    ReceivedString = "这里有问题5" + BotMessage.messages[1].text;
        //                    IsReplyReceived = true;
        //                }
        //            }
        //        }

        //    }
        //    return ReceivedString;
        //}
    }


    public class Conversation
    {
        public string conversationId { get; set; }
        public string token { get; set; }
        public string eTag { get; set; }
    }

    public class MessageSet
    {
        public Message[] messages { get; set; }
        public string watermark { get; set; }
        public string eTag { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public string conversationId { get; set; }
        public DateTime created { get; set; }
        public string from { get; set; }
        public string text { get; set; }
        public string channelData { get; set; }
        public string[] images { get; set; }
        public Attachment[] attachments { get; set; }
        public string eTag { get; set; }
    }

    public class Attachment
    {
        public string url { get; set; }
        public string contentType { get; set; }
    }


}

