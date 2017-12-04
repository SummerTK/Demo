using System;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace Microsoft.Bot.Sample.LuisBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                       //对话
                        await Conversation.SendAsync(activity, () => new BasicLuisDialog());
                        break;
                    //case ActivityTypes.ConversationUpdate:
                    //    if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                    //    {
                    //        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    //        Activity replyToConversation = activity.CreateReply("老铁您好，我是小哈，请问有什么可以帮到您的吗？");
                    //        //replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    //        //replyToConversation.Attachments = new List<Attachment>();

                    //        //Dictionary<string, string> cardContentList = new Dictionary<string, string>();
                    //        //cardContentList.Add("唐嫣", HttpContext.Current.Server.MapPath("../Images/timg.jpg"));
                    //        //cardContentList.Add("李沁", HttpContext.Current.Server.MapPath("../Images/li.jpg"));
                    //        //cardContentList.Add("张馨予", HttpContext.Current.Server.MapPath("../Images/zhang.jpg"));

                    //        //foreach (KeyValuePair<string, string> cardContent in cardContentList)
                    //        //{
                    //        //    List<CardImage> cardImages = new List<CardImage>();
                    //        //    cardImages.Add(new CardImage(url: cardContent.Value));

                    //        //    List<CardAction> cardButtons = new List<CardAction>();

                    //        //    CardAction plButton = new CardAction()
                    //        //    {
                    //        //        Value = $"{cardContent.Key}",
                    //        //        Type = "imBack",
                    //        //        Title = "查看"
                    //        //    };

                    //        //    cardButtons.Add(plButton);

                    //        //    HeroCard plCard = new HeroCard()
                    //        //    {
                    //        //        Title = $"中国人气旺的女明星—— {cardContent.Key}",
                    //        //        Subtitle = $"{cardContent.Key}",
                    //        //        Images = cardImages,
                    //        //        Buttons = cardButtons
                    //        //    };

                    //        //    Attachment plAttachment = plCard.ToAttachment();
                    //        //    replyToConversation.Attachments.Add(plAttachment);
                    //        //}

                    //        var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
                    //    }
                    //    break;
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    case ActivityTypes.Ping:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
        //public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        //{
        //    // check if activity is of type message
           
        //    else
        //    {
        //        HandleSystemMessage(activity);
        //    }
        //    return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        //}

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}