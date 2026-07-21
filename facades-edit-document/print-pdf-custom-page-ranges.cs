using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the viewer and bind the PDF file
        using (var viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Prepare printer settings – use the default system printer
            var printerSettings = new PrinterSettings
            {
                // Obtain the default printer name from System.Drawing.Printing.PrintDocument
                PrinterName = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName,
                PrintRange = PrintRange.SomePages // we will set FromPage/ToPage for each range
            };

            // Prepare page settings (optional – here we use A4 with no margins)
            var pageSettings = new PageSettings
            {
                PaperSize = PaperSizes.A4,
                Margins = new Margins(0, 0, 0, 0)
            };

            // ----- First page range: 1‑5 -----
            printerSettings.FromPage = 1;
            printerSettings.ToPage   = 5;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // ----- Second page range: 8‑10 -----
            printerSettings.FromPage = 8;
            printerSettings.ToPage   = 10;
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Printing completed for the specified page ranges.");
    }
}