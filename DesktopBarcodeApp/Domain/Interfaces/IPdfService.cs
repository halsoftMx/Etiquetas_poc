using DesktopBarcodeApp.Domain.Models;

namespace DesktopBarcodeApp.Domain.Interfaces
{
    public interface IPdfService
    {
        string GenerarPdf(EtiquetaModel model, string ruta);
    }
}
