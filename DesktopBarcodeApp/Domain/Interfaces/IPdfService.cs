using DesktopBarcodeApp.Domain.Models;

namespace DesktopBarcodeApp.Domain.Interfaces
{
    public interface IPdfService
    {
        void GenerarPdf(EtiquetaModel model, string ruta);
    }
}
