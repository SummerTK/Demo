using System.Collections.Generic;
using System.IO;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.Context;
using Wechat.Model;
using System.Threading.Tasks;

namespace Wechat
{
    public partial class CustomMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel) : base(inputStream, postModel)
        {
        }

        public CustomMessageHandler(RequestMessageBase requestMessage) : base(requestMessage)
        {
        }

        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            return base.OnEventRequest(requestMessage);
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            return responseMessage;
        }

        /// <summary>
        /// 文字类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></retturns>
        public  override  IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = MSBot.PostMessage(requestMessage.Content).Result;//用bot框架实现
            //responseMessage.Content = LuisAPI.PostMessage(requestMessage.Content);//用Luis实现
            return responseMessage;
        }

        /// <summary>
        /// 语音类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            if (!string.IsNullOrEmpty(requestMessage.Recognition))
            {
                responseMessage.Content = MSBot.PostMessage(requestMessage.Recognition).Result;
            }
            else
            {
                responseMessage.Content = "小哈没听清您说的是什么！";
            }
            return responseMessage;
        }

        /// <summary>
        /// 图片类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage) {
            var responseMessage = base.CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

        /// <summary>
        /// 视频类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

        /// <summary>
        /// 链接消息类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageImage>();
            return responseMessage;
        }

        /// <summary>
        /// 位置类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageImage>();
            return responseMessage;
        }
        
        /// <summary>
        /// 小视频类型请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

    }
}