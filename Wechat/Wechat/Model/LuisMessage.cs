using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wechat.Model
{
    public class TopScoringIntent
    {
        public string intent { get; set; }
        public double score { get; set; }
    }

    public class IntentsItem
    {
        public string intent { get; set; }
        public double score { get; set; }
    }

    public class EntitiesItem
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public double score { get; set; }
    }

    public class LuisMessage
    {
        public string query { get; set; }
        public TopScoringIntent topScoringIntent { get; set; }
        public List<IntentsItem> intents { get; set; }
        public List<EntitiesItem> entities { get; set; }

    }
}
