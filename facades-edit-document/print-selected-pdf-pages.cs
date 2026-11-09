using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a PdfViewer – Aspose.Pdf.Printing types are required, not System.Drawing.Printing
        using (var viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);
            viewer.AutoResize = true;      // optional – fit page to printer area
            viewer.AutoRotate = true;      // optional – rotate if needed
            viewer.PrintPageDialog = false; // silent print

            // ---------- Print pages 1‑5 ----------
            var printerSettings = new PrinterSettings
            {
                PrintRange = PrintRange.SomePages,
                FromPage = 1,
                ToPage = 5,
                Copies = 1
            };
            var pageSettings = new PageSettings(printerSettings) { Landscape = false };
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // ---------- Print pages 8‑10 ----------
            printerSettings.FromPage = 8;
            printerSettings.ToPage = 10;
            // Re‑use the same pageSettings (or create a new one if you need different orientation)
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted for pages 1‑5 and 8‑10.");
    }
}
