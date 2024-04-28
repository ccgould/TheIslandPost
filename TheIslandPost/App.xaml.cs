namespace TheIslandPost;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE0NjU2OEAzMjM0MmUzMDJlMzBiUFhzMXhXUTZyWkp1Z2R1QlJPN0ZUMkp3YVYwZktwMzhNUnJlOUd3RjA0PQ ==");

        MainPage = new AppShell();
    }
}
