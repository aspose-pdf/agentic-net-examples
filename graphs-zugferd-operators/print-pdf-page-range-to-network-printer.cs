using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace for Document handling

class PrintPdfExample
{
    static void Main()
    {
        // Path to the PDF file to be printed
        const string pdfPath = "input.pdf";

        // Network printer name (replace with actual printer name or URI)
        const string printerName = @"\\NetworkPrinter\Printer";

        // Page range to print (1‑based indexing)
        const int fromPage = 2;
        const int toPage   = 5;

        // Desired paper size (width and height in hundredths of an inch)
        // Example: A4 size = 827 x 1169 (approx 8.27" x 11.69")
        const int paperWidth  = 827;
        const int paperHeight = 1169;

        // Duplex mode: Vertical (short‑edge binding) or Horizontal (long‑edge binding)
        // Use Aspose.Pdf.Printing.Duplex.Vertical for two‑sided short‑edge printing
        var duplexMode = Aspose.Pdf.Printing.Duplex.Vertical;

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Initialize the PDF viewer facade (fully qualified to avoid using the Facades namespace)
            using (var viewer = new Aspose.Pdf.Facades.PdfViewer())
            {
                // Bind the PDF document
                viewer.BindPdf(pdfPath);

                // Optional viewer settings
                viewer.AutoResize = true;      // Fit content to printable area
                viewer.AutoRotate = true;      // Auto‑rotate pages if needed
                viewer.PrintPageDialog = false; // Suppress the page‑range dialog

                // Configure printer settings (fully qualified)
                var printerSettings = new Aspose.Pdf.Printing.PrinterSettings
                {
                    PrinterName = printerName,
                    FromPage = fromPage,
                    ToPage = toPage,
                    PrintRange = Aspose.Pdf.Printing.PrintRange.SomePages,
                    Duplex = duplexMode,
                    Copies = 1,
                    Collate = true
                };

                // Configure page settings (paper size and margins)
                var pageSettings = new Aspose.Pdf.Printing.PageSettings
                {
                    PaperSize = new Aspose.Pdf.Printing.PaperSize("CustomA4", paperWidth, paperHeight),
                    Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0) // zero margins
                };

                // Send the document to the specified printer with the defined settings
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            }

            Console.WriteLine("Print job submitted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}