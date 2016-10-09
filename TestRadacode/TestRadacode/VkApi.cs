using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TestRadacode
{
    class VKAPi
    {
        /* 
        static public async Task<List<string» Universitie(string methodName, Dictionary<string, string> parameters) 
        { 
        string answer = null; 
        var wer = await Invoke(methodName, parameters); 

        return new List<string>(); 
        } 
        //*/
        static public async Task<Dictionary<string, string>> Countries()
        {
            Dictionary<string, string> Countres = new Dictionary<string, string>();
            JToken ArrayCountry = await Call("database.getCountries", new Dictionary<string, string> {
{ "code", "" },
{ "offset",""},
{ "count", "1000" },
{ "need_all", "1" }});
            //var fir=ArrayCountry.ToObject<Dictionary<string, object»(); 
            foreach (var value in ArrayCountry)
            {
                string cid = ((JProperty)value.First).Value.ToString();
                string tit = ((JProperty)value.Last).Value.ToString();
                Countres.Add(tit,cid );
                //Console.WriteLine(value); 
            }
            return Countres;
        }
        static public async Task<Dictionary<string, string>> University(string str)
        {
            Dictionary<string, string> Countres = new Dictionary<string, string>();
            JToken ArrayCountry = await Call("database.getUniversities", new Dictionary<string, string> {
{ "q", str },
{ "count", "1000" }});
            //var fir=ArrayCountry.ToObject<Dictionary<string, object»(); 
            foreach (var value in ArrayCountry)
            {
                try
                {
                string cid = ((JProperty)value.First).Value.ToString();
                string tit = ((JProperty)value.First.Next).Value.ToString();
                Countres.Add(tit,cid );

                }
                catch
                {
                }
                //Console.WriteLine(value); 
            }
            return Countres;
        }
        static public async Task<Dictionary<string, string>> City(string str, string ch)
        {
            Dictionary<string, string> Countres = new Dictionary<string, string>();
            JToken ArrayCountry = await Call("database.getCities", new Dictionary<string, string> {
{ "country_id", str },
{ "q", ch },
{ "count", "1000" },
{ "need_all", "1" }});
            //var fir=ArrayCountry.ToObject<Dictionary<string, object»(); 
            foreach (var value in ArrayCountry)
            {
                string cid = ((JProperty)value.First).Value.ToString();
                string tit = ((JProperty)value.First.Next).Value.ToString();
                try
                {
                    tit += " " + ((JProperty)value.First.Next.Next).Value.ToString();
                    tit += " " + ((JProperty)value.First.Next.Next.Next).Value.ToString();

                }
                catch
                {
                }
                Countres.Add(tit,cid );
                //Console.WriteLine(value); 
            }
            return Countres;
        }
        static public async Task<Dictionary<string, string>> City(string str, int ch)
        {
            Dictionary<string, string> Countres = new Dictionary<string, string>();
            JToken ArrayCountry = await Call("database.getCities", new Dictionary<string, string> {
{ "country_id", str },
{ "offset", ch.ToString() },
{ "count", "1000" },
{ "need_all", "1" }});
            //var fir=ArrayCountry.ToObject<Dictionary<string, object»(); 
            foreach (var value in ArrayCountry)
            {
                string cid = ((JProperty)value.First).Value.ToString();
                string tit = ((JProperty)value.First.Next).Value.ToString();
                try
                {
                    tit += " " + ((JProperty)value.First.Next.Next).Value.ToString();
                    tit += " " + ((JProperty)value.First.Next.Next.Next).Value.ToString();

                }
                catch
                {
                }
                Countres.Add(tit, cid);
                //Console.WriteLine(value); 
            }
            return Countres;
        }

        static async Task<string> GetData(string str)
        {
            string data = "";
            try
            {

                var http = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri("https://api.vk.com/") };
                var resp = await http.GetAsync(str);
                data = await resp.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
            }
            return data;
        } 

/// <summary> 
/// Прямой вызов API-метода 
/// </summary> 
/// <param name="methodName">Название метода. Например, "wall.get".</param> 
/// <param name="parameters">Вход. параметры метода.</param> 
/// <param name="skipAuthorization">Флаг, что метод можно вызывать без авторизации.</param> 
/// <exception cref="AccessTokenInvalidException"></exception> 
/// <returns>Ответ сервера в формате JSON.</returns> 

static async Task<JToken> Call(string methodName, IDictionary<string, string> parameters)
        {

            string url = GetApiUrl(methodName, parameters, skipAuthorization: true);
            string answer = await GetData(url.Replace("\'", "%27"));
            return JObject.Parse(answer)["response"];
        }


        /// <summary> 
        /// Получить URL для API. 
        /// </summary> 
        /// <param name="methodName">Название метода.</param> 
        /// <param name="parameters">Параметры.</param> 
        /// <param name="skipAuthorization">Пропускать ли авторизацию</param> 
        /// <returns></returns> 
        static string GetApiUrl(string methodName, IDictionary<string, string> parameters, bool skipAuthorization = false)
        {
            var builder = new StringBuilder($"method/{methodName}?");

            foreach (var pair in parameters)
            {
                builder.AppendFormat($"{pair.Key}={pair.Value}&");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}
