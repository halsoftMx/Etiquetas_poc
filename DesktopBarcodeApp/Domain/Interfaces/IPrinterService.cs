using DesktopBarcodeApp.Domain.Models;

namespace DesktopBarcodeApp.Domain.Interfaces
{
    public interface IPrinterService
    {
        void Imprimir(EtiquetaModel model);
    }
}
