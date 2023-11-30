namespace maui_repro
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SidesheetPage), typeof(SidesheetPage));
        }
    }
}
