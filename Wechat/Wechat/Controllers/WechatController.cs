using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using System.IO;

namespace Wechat.Controllers
{
    public class WechatController : Controller
    {
        public static readonly string Token = "weixin";//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "6SSyRtjNjjFfzuogx5oUn4gYImdoCIkGYZbptVAnlEO";//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = "wx35989f7beb9b3c69";//与微信公众账号后台的AppId设置保持一致，区分大小写。

        // GET: WC/WeChat
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
        {
            return Task.Factory.StartNew(() =>
            {
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    return echostr; //返回随机字符串则表示验证通过
                }
                else
                {
                    return "failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                        "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                }
            }).ContinueWith<ActionResult>(task => Content(task.Result));
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            postModel.Token = Token;//根据自己后台的设置保持一致  
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致  
            postModel.AppId = AppId;//根据自己后台的设置保持一致  

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。  
            //获取request的响应  
            var memoryStream = new MemoryStream();
            Request.Body.CopyTo(memoryStream);
            var messageHandler = new CustomMessageHandler(memoryStream, postModel);//接收消息  
            messageHandler.Execute();//执行微信处理过程  

            return new FixWeixinBugWeixinResult(messageHandler);

        }

    }
}