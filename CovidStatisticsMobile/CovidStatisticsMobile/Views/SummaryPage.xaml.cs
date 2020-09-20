using CovidStatisticsMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidStatisticsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryPage : ContentPage
    {
        SummaryViewModel summary;
        public SummaryPage()
        {
            InitializeComponent();

            summary = new SummaryViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await summary.GetCovid();

            BindingContext = summary;
        }
    }
}