using System;
using System.IO;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Obtain the default system printer name
        var systemPrintDoc = new PrintDocument();
        string defaultPrinterName = systemPrintDoc.PrinterSettings.PrinterName;

        // Configure Aspose.Pdf printer settings
        var printerSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            PrinterName = defaultPrinterName,
            Copies = 2,                     // number of copies to print
            Collate = true,                 // enable collated printing
            Duplex = Aspose.Pdf.Printing.Duplex.Vertical // double‑sided (short‑edge flip)
        };

        // Bind the PDF and print with the specified settings
        using (var viewer = new Aspose.Pdf.Facades.PdfViewer())
        {
            viewer.BindPdf(inputPath);
            viewer.PrintPageDialog = false; // suppress the native print dialog
            viewer.PrintDocumentWithSettings(printerSettings);
            viewer.Close();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}