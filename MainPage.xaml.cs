using System.Diagnostics;

namespace maui_repro
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SidesheetPage));
        }
       
    }

}
