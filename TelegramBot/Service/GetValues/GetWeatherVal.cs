using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Service.GetValues
{
    /*
    * Получение данных о погоде
    */
    class GetWeatherVal
    {
        private readonly string apiKey = "908bf0611586f5cbdefb8540ccf08a54";
        private readonly string city = "Набережные Челны";
        public async Task<string> GetWeatherAsync()
        {
            try
            {
                var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&lang=ru";
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
                        string weatherMessage = $"Город: {weatherData.name}\n" +
                                                $"Температура: {Math.Round(weatherData.main.temp - 273.15, 0)}°C\n" +
                                                $"Погода: {weatherData.weather[0].description}\n" +
                                                $"Ощущается как: {Math.Round(weatherData.main.feels_like - 273.15, 0)}°C";

                        return weatherMessage;
                    }
                    else
                    {
                        return $"Ошибка при получении данных о погоде. Код состояния: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Произошла ошибка при получении данных о погоде: {ex.Message}";
            }
        }
    }
}

