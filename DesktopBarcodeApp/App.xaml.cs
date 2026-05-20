using QuestPDF.Infrastructure;

namespace DesktopBarcodeApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
    }
}
