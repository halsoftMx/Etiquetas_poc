using DesktopBarcodeApp.Domain.Interfaces;
using DesktopBarcodeApp.Domain.Models;
using QuestPDF.Fluent;
using System.IO;

namespace DesktopBarcodeApp.Application.Services;

public class PdfService : IPdfService
{
    public string GenerarPdf(EtiquetaModel model, string ruta)
    {
        BarcodeService barcodeService = new BarcodeService();

        // ==============================
        // GENERAR BARCODES
        // ==============================
        var barcodeIzquierdo = barcodeService.GenerarCode128Vertical(model.Codigo);
        var barcodeDerecho = barcodeService.GenerarCode128Vertical(model.Codigo);
        var barcodeInferior = barcodeService.GenerarCode128Horizontal(model.Codigo);

        // ==============================
        // GENERAR QR
        // ==============================
        var qr = barcodeService.GenerarQr(model.Codigo);

        // =========================================
        // RUTAS TEMPORALES
        // =========================================
        string temp = Path.GetTempPath();
        string izquierdaPath = Path.Combine(temp, "izquierda.png");
        string derechaPath = Path.Combine(temp, "derecha.png");
        string inferiorPath = Path.Combine(temp, "inferior.png");
        string qrPath = Path.Combine(temp, "qr.png");


        // =========================================
        // GUARDAR IMAGENES
        // =========================================
        barcodeIzquierdo.Save(izquierdaPath, System.Drawing.Imaging.ImageFormat.Png);
        barcodeDerecho.Save(derechaPath, System.Drawing.Imaging.ImageFormat.Png);
        barcodeInferior.Save(inferiorPath, System.Drawing.Imaging.ImageFormat.Png);
        qr.Save(qrPath, System.Drawing.Imaging.ImageFormat.Png);

        // ==============================
        // CREAR PDF
        // ==============================
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(450, 600);

                page.Margin(10);

                page.Content().Row(row =>
                {
                    // =====================================
                    // BARCODE IZQUIERDO
                    // =====================================
                    row.ConstantItem(55).Height(260).Row(inner =>
                    {
                        // ==========================
                        // TEXTO IZQUIERDO
                        // ==========================
                        inner.ConstantItem(12).Height(260).PaddingBottom(65).AlignCenter().AlignMiddle().RotateLeft().Text(model.Codigo).FontSize(7).Bold();

                        // ==========================
                        // BARCODE IZQUIERDO
                        // ==========================
                        inner.RelativeItem().Element(container =>
                        {
                            container.ScaleToFit().Image(File.ReadAllBytes(izquierdaPath));
                        });
                    });

                    // =====================================
                    // CONTENIDO CENTRAL
                    // =====================================
                    row.RelativeItem().PaddingHorizontal(15).Column(column =>
                    {
                        column.Item().Text(model.Empresa).Bold().FontSize(12);
                        column.Item().Text(model.Direccion);
                        column.Item().Text(model.Ciudad);

                        column.Item().PaddingTop(10);

                        column.Item().Text(model.Producto).FontSize(14).Bold();
                        column.Item().Text(model.Composicion);

                        column.Item().PaddingTop(20);

                        column.Item().AlignCenter().Text(model.Metros).FontSize(28).Bold();
                        column.Item().AlignCenter().Text("METROS");

                        column.Item().PaddingTop(20);

                        column.Item().AlignCenter().Text(model.Origen);

                        column.Item().PaddingTop(20);

                        // ===============================
                        // BARCODE INFERIOR
                        // ===============================
                        column.Item().PaddingTop(20).AlignCenter().Width(220).Column(barcode =>
                        {
                            barcode.Item().Height(50).Element(container =>
                            {
                                container.ScaleToFit().Image(File.ReadAllBytes(inferiorPath));
                            });

                            barcode.Item().AlignCenter().Text(model.Codigo).FontSize(10);
                        });

                        // ===============================
                        // QR INFERIOR
                        // ===============================
                        //column.Item().PaddingTop(20).AlignCenter().Width(90).Height(90).Image(qrPath);
                    });

                    // =====================================
                    // BARCODE DERECHO
                    // =====================================
                    //row.ConstantItem(55).Height(260).Row(inner =>
                    //{
                    //    // ==========================
                    //    // BARCODE DERECHO
                    //    // ==========================
                    //    inner.RelativeItem().Element(container =>
                    //    {
                    //        container.ScaleToFit().Image(File.ReadAllBytes(derechaPath));
                    //    });

                    //    // ==========================
                    //    // TEXTO DERECHO
                    //    // ==========================
                    //    inner.ConstantItem(12).Height(260).PaddingBottom(65).AlignCenter().AlignMiddle().RotateRight().Text(model.Codigo).FontSize(7).Bold();
                    //});

                    // ===============================
                    // QR DERECHO
                    // ===============================
                    row.ConstantItem(90).Height(260).Column(column =>
                    {
                        // ==========================
                        // QR DERECHO
                        // ==========================
                        column.Item().AlignCenter().Width(80).Height(80).Element(container =>
                        {
                            container.ScaleToFit().Image(File.ReadAllBytes(qrPath));
                        });

                        // ==========================
                        // TEXTO QR DERECHO
                        // ==========================
                        column.Item().PaddingTop(5).AlignCenter().Text(model.Codigo).FontSize(7).Bold();
                    });
                });
            });
        }).GeneratePdf(ruta);

        return ruta;
    }
}