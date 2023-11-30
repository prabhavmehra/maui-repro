namespace maui_repro;

public partial class CustomView : ContentView
{
    public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(CustomView), Colors.White);
    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(IView), typeof(CustomView));
    public static readonly BindableProperty BodyProperty = BindableProperty.Create(nameof(Body), typeof(IView), typeof(CustomView));

    public Color HeaderBackgroundColor
    {
        get => (Color)GetValue(HeaderBackgroundColorProperty);
        set => SetValue(HeaderBackgroundColorProperty, value);
    }

    public IView Header
    {
        get => (IView)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public IView Body
    {
        get => (IView)GetValue(BodyProperty);
        set => SetValue(BodyProperty, value);
    }


    Border sidesheet;


    bool originalTemplate = true;
    public bool OriginalTemplate
    {
        get { return originalTemplate; }
    }

    public CustomView()
    {
        InitializeComponent();
    }

    private bool isSidesheetVisible = false;

    void ToggleSidesheet(object sender, EventArgs e)
    {
        isSidesheetVisible = !isSidesheetVisible;

        double translationX = isSidesheetVisible ? 0 : sidesheet.Width; // Slide in from the right
        sidesheet.TranslateTo(translationX, 0, 250, Easing.SinInOut);

        // Toggle visibility of the sidesheet content
        sidesheet.IsVisible = isSidesheetVisible;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        sidesheet = (Border)GetTemplateChild("sidesheet");
        sidesheet.IsVisible = OriginalTemplate ? false : true;
    }

}