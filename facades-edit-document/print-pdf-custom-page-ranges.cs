using System;
using System.IO;
using System.Drawing.Printing; // for PrintDocument to obtain default printer name
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Obtain the default printer name from the system PrintDocument
        var sysPrintDoc = new System.Drawing.Printing.PrintDocument();
        string defaultPrinter = sysPrintDoc.PrinterSettings.PrinterName;

        // Configure printer settings (Aspose.Pdf.Printing.PrinterSettings)
        var printerSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            PrinterName = defaultPrinter,
            PrintRange = Aspose.Pdf.Printing.PrintRange.SomePages,
            // FromPage/ToPage will be set before each print call
        };

        // Configure page settings (paper size A4, zero margins)
        var pageSettings = new Aspose.Pdf.Printing.PageSettings
        {
            PaperSize = Aspose.Pdf.Printing.PaperSizes.A4,
            Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
        };

        // Create the viewer and bind the PDF
        using (var viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPdf);
            viewer.AutoResize = true;   // fit to printable area
            viewer.AutoRotate = true;   // rotate if needed
            viewer.PrintPageDialog = false; // suppress page dialog

            // ---- Print pages 1‑5 ----
            printerSettings.FromPage = 1;
            printerSettings.ToPage   = 5;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // ---- Print pages 8‑10 ----
            printerSettings.FromPage = 8;
            printerSettings.ToPage   = 10;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Printing completed.");
    }
}