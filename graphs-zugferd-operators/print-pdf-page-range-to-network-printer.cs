using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class PrintPdfExample
{
    static void Main()
    {
        // Input parameters
        const string pdfPath = "input.pdf";                 // Path to the PDF file
        const string printerName = @"\\NetworkPrinter\Printer"; // Network printer name or URI
        const int fromPage = 2;                             // First page to print (1‑based)
        const int toPage = 5;                               // Last page to print (inclusive)
        const Duplex duplexMode = Duplex.Vertical;          // Duplex setting (Vertical = short‑edge flip)
        const string paperSizeName = "A4";                  // Paper size identifier
        const int paperWidth = 827;                         // Width in hundredths of an inch (A4)
        const int paperHeight = 1169;                       // Height in hundredths of an inch (A4)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the PDF viewer (facade) without importing the Aspose.Pdf.Facades namespace
        var viewer = new Aspose.Pdf.Facades.PdfViewer();

        try
        {
            // Bind the PDF document to the viewer
            viewer.BindPdf(pdfPath);

            // Configure printer settings
            var printerSettings = new PrinterSettings
            {
                PrinterName = printerName,
                FromPage = fromPage,
                ToPage = toPage,
                PrintRange = PrintRange.SomePages,
                Duplex = duplexMode,
                // Optional: number of copies, collate, etc.
                Copies = 1,
                Collate = true
            };

            // Configure page settings (paper size, margins, etc.)
            var pageSettings = new PageSettings(printerSettings)
            {
                PaperSize = new PaperSize(paperSizeName, paperWidth, paperHeight),
                Margins = new Margins(0, 0, 0, 0) // No margins
            };

            // Print the specified page range with the defined settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released
            viewer.Close();
        }
    }
}