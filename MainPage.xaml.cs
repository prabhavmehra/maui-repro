using System.Diagnostics;

namespace maui_repro
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel();
        }

        //private async void OnCounterClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync(nameof(SidesheetPage));
        //}
        private bool isSidesheetVisible = false;

        void ToggleSidesheet(object sender, EventArgs e)
        {
            Debug.WriteLine("here clicked");
            isSidesheetVisible = !isSidesheetVisible;

            double translationX = isSidesheetVisible ? 0 : sidesheet.Width; // Slide in from the right
            sidesheet.TranslateTo(translationX, 0, 250, Easing.SinInOut);

            // Toggle visibility of the sidesheet content
            sidesheet.IsVisible = isSidesheetVisible;
        }
    }

}
