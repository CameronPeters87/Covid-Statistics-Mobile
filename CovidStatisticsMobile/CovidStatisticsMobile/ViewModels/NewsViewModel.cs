using CovidStatisticsMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CovidStatisticsMobile.ViewModels
{
    public class NewsViewModel
    {
        News news;
        List<Article> articles;
        List<ArticlesViewModel> articlesVM;

        public List<Article> Articles
        {
            get
            {
                return articles;
            }
        }

        public List<ArticlesViewModel> ArticlesVM
        {
            get
            {
                return articlesVM;
            }
        }


        public NewsViewModel()
        {
            articlesVM = new List<ArticlesViewModel>();
        }

        public async Task Init()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            using (var client = new HttpClient(httpClientHandler))
            {
                try
                {
                    var responseMessage = await client.GetAsync(new Uri("http://newsapi.org/v2/top-headlines?country=za&category=health&apiKey=8066df69f5c2435c9a6b0510ea7b16d5"));

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var newsResponse = responseMessage.Content
                            .ReadAsStringAsync().Result;

                        news = JsonConvert.DeserializeObject<News>(newsResponse);
                        articles = GetArticlesWithImages(news);

                        GetArticles(Articles);
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public List<Article> GetArticlesWithImages(News news)
        {
            return news.articles.Where(a => !string.IsNullOrEmpty(a.urlToImage)).ToList();
        }

        private List<ArticlesViewModel> GetArticles(List<Article> articles)
        {
            foreach (var item in articles)
            {
                articlesVM.Add(new ArticlesViewModel
                {
                    author = item.author,
                    content = item.content,
                    description = FormatDescription(item.description, 150),
                    title = item.title,
                    urlToImage = item.urlToImage,
                    publishedAt = item.publishedAt.ToLongDateString(),
                    source = item.source,
                    url = item.url
                });
            }

            articles.OrderByDescending(article => article.publishedAt);

            return articlesVM;
        }

        private string FormatDescription(string description, int length)
        {
            description = Regex.Replace(description, @"<[^>]+>|&nbsp;", string.Empty).Trim();

            if (description.Length > length)
            {
                return string.Format("{0}...", description.Substring(0, length));
            }
            else
                return description;
        }
    }
}