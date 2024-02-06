using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Service.GetValues
{
    /*
    * Получение курса Доллар,Евро,Юань
    */
    class GetCurrencyVal
    {
        public async Task<string> GetCurrencyAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "https://www.cbr-xml-daily.ru/daily_json.js";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject data = JObject.Parse(responseBody);

                    string usdRate = GetCurrencyRate(data, "USD"); // Курс доллара к рублю
                    string eurRate = GetCurrencyRate(data, "EUR"); // Курс евро к рублю
                    string cnyRate = GetCurrencyRate(data, "CNY"); // Курс юаня к рублю

                    string currencyResult = $"Курс доллара - {usdRate}\n" +
                                            $"Курс евро - {eurRate}\n" +
                                            $"Курс юаня - {cnyRate}";
                    return currencyResult;
                }
            }
            catch (Exception ex)
            {
                return $"ошибка {ex.Message}";
            }
        }

        static string GetCurrencyRate(JObject data, string currencyCode)
        {
            string rate = "Нет данных";
            try
            {
                rate = data["Valute"][currencyCode]["Value"].ToString();
            }
            catch (Exception)
            {
                rate = "Данные отсутствуют";
            }
            return rate;
        }
    }
}
