using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            string weatherAPI = "http://api.openweathermap.org/data/2.5/weather?q=Nuevo%20Leon,MX-NLE&appid=2463be2ddf888bd1b3efaef22e7ecd99";
            var weatherClient = new HttpClient();
            var weatherResponse = weatherClient.GetStringAsync(weatherAPI).Result;

            var weatherDescription = JObject.Parse(weatherResponse).GetValue("weather").ToString();
            var weatherTemperature = JObject.Parse(weatherResponse).GetValue("main").ToString();
            var weatherCity = JObject.Parse(weatherResponse).GetValue("name").ToString();

            //Remove Strings
            string removeInDescription = "\"description\": \"";
            string removeInTemperature = "\"temp\": ";

            //Remove Index
            int descriptionIndex = weatherDescription.IndexOf(removeInDescription);
            int temperatureIndex = weatherTemperature.IndexOf(removeInTemperature);

            //Remove unecesary values
            //From Description
            weatherDescription = weatherDescription.Remove(0, descriptionIndex + removeInDescription.Length);
            descriptionIndex = weatherDescription.IndexOf("\"");

            weatherDescription = weatherDescription.Remove(descriptionIndex, weatherDescription.Length - descriptionIndex);

            //From Temperature
            weatherTemperature = weatherTemperature.Remove(0, temperatureIndex + removeInTemperature.Length);
            temperatureIndex = weatherTemperature.IndexOf(",");

            weatherTemperature = weatherTemperature.Remove(temperatureIndex, weatherTemperature.Length - temperatureIndex);

            Console.WriteLine($"The weather in {weatherCity} is {weatherTemperature}°F and the status of {weatherDescription}!");

        }
    }
}
