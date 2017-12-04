using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using BOPdemo.Models;
using BOPdemo;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [LuisModel("13526516-0de0-4ef4-a617-4ab3ae6b9c75", "ed92b9e259df4b2ab49e1b976ad0a739")]
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task M_None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"不好意思老铁,您的问题小哈不是太理解，您可以给小哈说细一点。"); //
            context.Wait(MessageReceived);
        }

        [LuisIntent("查询天气")]
        public async Task QueryWeather(IDialogContext context, LuisResult result)
        {
            string location = "";
            string replyString = "";
            if (TryToFindLocation(result,"", out location))
            {
                replyString = await GetWeather(location);
                await context.PostAsync(replyString);
                context.Wait(MessageReceived);
            }
            else
            {
                await M_None(context, result);
            }
        }
        private async Task<string> GetWeather(string cityname)
        {
            WeatherData weatherdata = await BOPdemoTask.GetWeatherAsync(cityname);
            if (weatherdata == null || weatherdata.HeWeatherdataservice30 == null)
            {
                return string.Format("呃。。。小哈不知道\"{0}\"这个城市的天气信息", cityname);
            }
            else
            {
                HeweatherDataService30[] weatherServices = weatherdata.HeWeatherdataservice30;
                if (weatherServices.Length <= 0) return string.Format("呃。。。小哈不知道\"{0}\"这个城市的天气信息", cityname);
                Basic cityinfo = weatherServices[0].basic;
                if (cityinfo == null) return string.Format("呃。。。小哈目测\"{0}\"这个应该不是一个城市的名字。。不然我咋不知道呢。。。", cityname);
                String cityinfoString = "城市信息：" + cityinfo.city + "\n\n"
                    + "更新时间：" + cityinfo.update.loc + "\n\n"
                    + "经纬度：" + cityinfo.lat + "," + cityinfo.lon + "\n\n";
                Aqi cityAirInfo = weatherServices[0].aqi;
                String airInfoString = "空气质量指数：" + cityAirInfo.city.aqi + "\n\n"
                    + "PM2.5 1小时平均值：" + cityAirInfo.city.pm25 + "(ug/m³)\n\n"
                    + "PM10 1小时平均值：" + cityAirInfo.city.pm10 + "(ug/m³)\n\n"
                    + "二氧化硫1小时平均值：" + cityAirInfo.city.so2 + "(ug/m³)\n\n"
                    + "二氧化氮1小时平均值：" + cityAirInfo.city.no2 + "(ug/m³)\n\n"
                    + "一氧化碳1小时平均值：" + cityAirInfo.city.co + "(ug/m³)\n\n";

                Suggestion citySuggestion = weatherServices[0].suggestion;
                String suggestionString = "生活指数：" + "\n\n"
                    + "穿衣指数：" + citySuggestion.drsg.txt + "\n\n"
                    + "紫外线指数：" + citySuggestion.uv.txt + "\n\n"
                    + "舒适度指数：" + citySuggestion.comf.txt + "\n\n"
                    + "旅游指数：" + citySuggestion.trav.txt + "\n\n"
                    + "感冒指数：" + citySuggestion.flu.txt + "\n\n";

                Daily_Forecast[] cityDailyForecast = weatherServices[0].daily_forecast;
                Now cityNowStatus = weatherServices[0].now;
                String nowStatusString = "天气实况：" + "\n\n"
                    + "当前温度(摄氏度)：" + cityNowStatus.tmp + "\n\n"
                    + "体感温度：" + cityNowStatus.fl + "\n\n"
                    + "风速：" + cityNowStatus.wind.spd + "(Kmph)\n\n"
                    + "湿度：" + cityNowStatus.hum + "(%)\n\n"
                    + "能见度：" + cityNowStatus.vis + "(km)\n\n";

                return string.Format("现在{0}天气实况：\n\n{1}", cityname, cityinfoString + nowStatusString + airInfoString + suggestionString);
            }
        }


        /// <summary>
        /// 获取实体中的内容
        /// </summary>
        /// <param name="result"></param>
        /// <param name="Entity">实体对象名称</param>
        /// <param name="location">实体对象内容</param>
        /// <returns></returns>
        public bool TryToFindLocation(LuisResult result,string Entity, out String location)
        {
            location = "";
            EntityRecommendation title;
            if (result.TryFindEntity(Entity, out title))
            {
                location = title.Entity;
            }
            else
            {
                location = "";
            }
            return !location.Equals("");
        }
    }
}