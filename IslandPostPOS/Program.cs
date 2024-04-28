namespace IslandPostPOS;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE1NjM0NUAzMjM0MmUzMDJlMzBvTW9vL0ZWdEdmT1haUVJjY3Y0SWMxeGRuRUVPL0VBOHA2dVZjQUJOU0owPQ ==");
        Application.Run(new Form1());
    }
}