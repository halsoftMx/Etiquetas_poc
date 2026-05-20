using DesktopBarcodeApp.Domain.Interfaces;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace DesktopBarcodeApp.Application.Services;

public class BarcodeService : IBarcodeService
{

    // =========================================
    // BARCODE VERTICAL
    // =========================================
    public Bitmap GenerarCode128Vertical(string contenido)
    {
        BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.CODE_128,

            Options = new EncodingOptions
            {
                Width = 35,
                Height = 140,
                Margin = 1,
                PureBarcode = true
            },

            Renderer = new BitmapRenderer()
        };

        Bitmap bitmap = writer.Write(contenido);

        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);

        Bitmap resized = new Bitmap(bitmap, new Size(35, 140));

        return resized;
    }

    // =========================================
    // BARCODE HORIZONTAL
    // =========================================
    public Bitmap GenerarCode128Horizontal(string contenido)
    {
        BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.CODE_128,

            Options = new EncodingOptions
            {
                Width = 220,
                Height = 45,
                Margin = 1,
                PureBarcode = true
            },

            Renderer = new BitmapRenderer()
        };

        return writer.Write(contenido);
    }

    // =========================================
    // QR
    // =========================================
    public Bitmap GenerarQr(string contenido)
    {
        BarcodeWriter<Bitmap> writer = new BarcodeWriter<Bitmap>
        {
            Format = BarcodeFormat.QR_CODE,

            Options = new EncodingOptions
            {
                Width = 180,
                Height = 180,
                Margin = 1
            },

            Renderer = new BitmapRenderer()
        };

        return writer.Write(contenido);
    }
}