using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Dialogs
{
    public class QueryData
    {

    }

    public class Data
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Entity { get; set; }
        public string Answer { get; set; }
        public string Url { get; set; }
    }
}