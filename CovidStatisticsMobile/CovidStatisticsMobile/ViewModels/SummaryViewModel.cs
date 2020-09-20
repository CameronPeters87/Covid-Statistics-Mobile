using CovidStatisticsMobile.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidStatisticsMobile.ViewModels
{
    public class SummaryViewModel
    {
        Covid covid;
        CountryModel rsa;
        const string BASE_URL = "https://api.covid19api.com/";

        // Global
        public int TotalConfirmed
        {
            get
            {
                return covid.Global.TotalConfirmed;
            }
        }
        public int TotalRecovered
        {
            get
            {
                return covid.Global.TotalRecovered;
            }
        }
        public int TotalDead
        {
            get
            {
                return covid.Global.TotalDeaths;
            }
        }

        // South Africa
        public int RSAConfirmed
        {
            get
            {
                return rsa.TotalConfirmed;
            }
        }
        public int RSARecovered
        {
            get
            {
                return rsa.TotalRecovered;
            }
        }
        public int RSADead
        {
            get
            {
                return rsa.TotalDeaths;
            }
        }

        public async Task GetCovid()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                try
                {

                    var responseMessage = await client.GetAsync(new Uri("https://api.covid19api.com/summary"));

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var covidResponse = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        covid = JsonConvert.DeserializeObject<Covid>(covidResponse);
                        rsa = covid.Countries.Where(c => c.Country == "South Africa").FirstOrDefault();

                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }
    }
}
