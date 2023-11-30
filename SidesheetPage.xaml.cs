using System.Diagnostics;

namespace maui_repro;

public partial class SidesheetPage : ContentPage
{
	public SidesheetPage(ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        SizeChanged += SidesheetPage_SizeChanged;
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        Debug.WriteLine(viewModel.Monkeys.Count);
	}

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Debug.WriteLine(e.PropertyName);
    }

    private void SidesheetPage_SizeChanged(object? sender, EventArgs e)
    {
        Debug.WriteLine("Page: " + Width);
    }

    //private bool isSidesheetVisible = false;

    //void ToggleSidesheet(object sender, EventArgs e)
    //{
    //    Debug.WriteLine("here clicked");
    //    isSidesheetVisible = !isSidesheetVisible;

    //    double translationX = isSidesheetVisible ? 0 : sidesheet.Width; // Slide in from the right
    //    sidesheet.TranslateTo(translationX, 0, 250, Easing.SinInOut);

    //    // Toggle visibility of the sidesheet content
    //    sidesheet.IsVisible = isSidesheetVisible;
    //}
}