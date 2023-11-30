using System.Diagnostics;

namespace maui_repro;

public partial class SidesheetPage : ContentPage
{
	public SidesheetPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

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