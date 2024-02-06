using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Service.GetValues;

namespace TelegramBot.Service
{
    public class ServiceLogick
    {
        private readonly string[] checkWeather =
        {
            "weather","/weather","погода","пгда","прогноз","/погода","/прогноз"
        };
        private readonly string[] checkCurrency =
        {
            "/currency","currency","курс","рубль","рубля","доллар","евро","юань"
        };
        public async Task<string> GetValues(string messageText)
        {
            GetWeatherVal weather = new GetWeatherVal();
            GetCurrencyVal currency = new GetCurrencyVal();
            if (messageText.Contains(checkWeather.ToString()))
            {
                var weatherResult = await weather.GetWeatherAsync();
                return weatherResult;
            }
            else if (messageText.Contains(checkCurrency.ToString()))
            {
                var currencyResult = await currency.GetCurrencyAsync();
                return currencyResult;
            }
            else
            {
                return "Такой команды нет";
            }
        }
    }
}
