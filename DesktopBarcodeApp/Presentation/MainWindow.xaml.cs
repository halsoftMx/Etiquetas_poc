using DesktopBarcodeApp.Application.Services;
using DesktopBarcodeApp.Domain.Interfaces;
using DesktopBarcodeApp.Domain.Models;
using System.Windows;
using ZXing.QrCode.Internal;

namespace DesktopBarcodeApp.Presentation;

public partial class MainWindow : Window
{
    private readonly PrinterService _printerService;
    private readonly PdfService _pdfService;
    private string ultimoPdf = string.Empty;

    public MainWindow()
    {
        InitializeComponent();

        _printerService = new PrinterService("TSC TTP-244 Pro");
        _pdfService = new PdfService();

        CargarImpresoras();
    }

    private EtiquetaModel ObtenerModelo()
    {
        BarcodeService service = new BarcodeService();

        //return new EtiquetaModel
        //{
        //    Empresa = txtEmpresa.Text,
        //    Direccion = txtDireccion.Text,
        //    Ciudad = txtCiudad.Text,
        //    Producto = txtProducto.Text,
        //    Composicion = txtComposicion.Text,
        //    Origen = txtOrigen.Text,
        //    Codigo = txtCodigo.Text,
        //    CodigoInterno = txtCodigoInterno.Text,
        //    Metros = txtMetros.Text
        //};

        return  new EtiquetaModel
        {
            Empresa = "COMERCIALIZADORA KRIOSQ",
            Direccion = "CALLE RIO NO. 971",
            Ciudad = "MEXICALI BCN CP:21000",
            Producto = "TEJIDO PLANO RECICLADO",
            Composicion = "80% POLIESTER 20% ALGODON",
            Origen = "HECHO EN CHINA",
            Codigo = "4010214",
            CodigoInterno = "P-D63",
            Metros = "96.00"
        };
    }

    private void btnImprimir_Click(object sender, RoutedEventArgs e)
    {
        var modelo = ObtenerModelo();

        string ruta = @"C:\Etiquetas\Etiqueta_" + modelo.Codigo + ".pdf";

        ultimoPdf = _pdfService.GenerarPdf(modelo, ruta);

        //_printerService.Imprimir(modelo);

        //System.Windows.MessageBox.Show("Etiqueta enviada a impresión");

        if (string.IsNullOrEmpty(ultimoPdf))
        {
            System.Windows.MessageBox.Show("Primero genera el PDF");

            return;
        }

        _printerService.ImprimirPdf(ultimoPdf);
    }

    private void btnPdf_Click(object sender, RoutedEventArgs e)
    {
        var modelo = ObtenerModelo();

        string ruta = @"C:\Etiquetas\Etiqueta_" + modelo.Codigo + ".pdf";

        _pdfService.GenerarPdf(modelo, ruta);

        System.Windows.MessageBox.Show("PDF generado correctamente");
    }

    private void CargarImpresoras()
    {
        cmbPrinters.ItemsSource = _printerService.ObtenerImpresoras();

        cmbPrinters.SelectedIndex = 0;
    }
}