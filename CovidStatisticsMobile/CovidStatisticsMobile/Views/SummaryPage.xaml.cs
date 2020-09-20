using CovidStatisticsMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidStatisticsMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var summaryViewModel = new SummaryViewModel();
            await summaryViewModel.GetCovid();

            BindingContext = summaryViewModel;
        }
    }
}