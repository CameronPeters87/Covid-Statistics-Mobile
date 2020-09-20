using CovidStatisticsMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidStatisticsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        NewsViewModel news;

        public NewsPage()
        {
            InitializeComponent();

            news = new NewsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await news.Init();

            newsListView.ItemsSource = news.ArticlesVM;
            newsListView.Refreshing += NewsListView_Refreshing;
        }

        private void NewsListView_Refreshing(object sender, System.EventArgs e)
        {
            OnAppearing();
            newsListView.IsRefreshing = false;
        }
    }
}