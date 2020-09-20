using System.Collections.Generic;

namespace CovidStatisticsMobile.Models
{
    public class News
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public IList<Article> articles { get; set; }
    }
}
