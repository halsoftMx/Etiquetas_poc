using DesktopBarcodeApp.Domain.Interfaces;
using DesktopBarcodeApp.Domain.Models;
using DesktopBarcodeApp.Infrastructure.Printing;
using System.Diagnostics;
using System.Printing;
using System.Text;

namespace DesktopBarcodeApp.Application.Services;

public class PrinterService : IPrinterService
{
    private readonly string _printerName;

    public PrinterService(string printerName)
    {
        _printerName = printerName;
    }

    public List<string> ObtenerImpresoras()
    {
        List<string> impresoras = new();

        LocalPrintServer server = new LocalPrintServer();

        foreach (PrintQueue printer in server.GetPrintQueues())
        {
            impresoras.Add(printer.Name);
        }

        return impresoras;
    }

    public void ImprimirPdf(string pdfPath)
    {
        ProcessStartInfo info = new ProcessStartInfo
        {
            Verb = "print",
            FileName = pdfPath,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        Process.Start(info);
    }

    public void Imprimir(EtiquetaModel model)
    {
        string comando = ConstruirComando(model);

        RawPrinterHelper.SendStringToPrinter(_printerName, comando);
    }

    private string ConstruirComando(EtiquetaModel model)
    {
        StringBuilder tspl = new StringBuilder();

        tspl.AppendLine("SIZE 100 mm,70 mm");
        tspl.AppendLine("GAP 2 mm,0");
        tspl.AppendLine("DENSITY 6");
        tspl.AppendLine("SPEED 3");
        tspl.AppendLine("DIRECTION 1");
        tspl.AppendLine("REFERENCE 0,0");
        tspl.AppendLine("CLS");

        // Barcode izquierdo
        tspl.AppendLine($"BARCODE 10,40,\"128\",230,1,90,2,2,\"{model.Codigo}\"");

        // Barcode derecho
        tspl.AppendLine($"BARCODE 730,40,\"128\",230,1,90,2,2,\"{model.Codigo}\"");

        // Empresa
        tspl.AppendLine($"TEXT 110,20,\"2\",0,1,1,\"{model.Empresa}\"");
        tspl.AppendLine($"TEXT 110,50,\"2\",0,1,1,\"{model.Direccion}\"");
        tspl.AppendLine($"TEXT 110,80,\"2\",0,1,1,\"{model.Ciudad}\"");

        // Producto
        tspl.AppendLine($"TEXT 110,150,\"3\",0,1,1,\"{model.Producto}\"");
        tspl.AppendLine($"TEXT 110,190,\"2\",0,1,1,\"{model.Composicion}\"");

        // Metros
        tspl.AppendLine($"TEXT 470,20,\"5\",0,5,5,\"{model.Metros}\"");
        tspl.AppendLine("TEXT 560,120,\"3\",0,2,2,\"METROS\"");

        // Origen
        tspl.AppendLine($"TEXT 260,300,\"3\",0,1,1,\"{model.Origen}\"");

        // Codigo interno
        tspl.AppendLine($"TEXT 110,300,\"3\",0,1,1,\"{model.CodigoInterno}\"");

        // Barcode inferior
        tspl.AppendLine($"BARCODE 170,370,\"128\",110,1,0,3,3,\"{model.Codigo}\"");

        // Texto inferior
        tspl.AppendLine($"TEXT 240,500,\"2\",0,1,1,\"*{model.Codigo}*\"");

        tspl.AppendLine("PRINT 1");

        return tspl.ToString();
    }
}