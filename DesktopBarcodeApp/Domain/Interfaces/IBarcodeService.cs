namespace DesktopBarcodeApp.Domain.Interfaces
{
    public interface IBarcodeService
    {
        Bitmap GenerarCode128Vertical(string contenido);

        Bitmap GenerarCode128Horizontal(string contenido);

        Bitmap GenerarQr(string contenido);
    }
}
