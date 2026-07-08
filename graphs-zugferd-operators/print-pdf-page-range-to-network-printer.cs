using System;
using System.IO;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Define the page range to print (1‑based indexing)
        int fromPage = 2;   // first page to print
        int toPage   = 5;   // last page to print

        // Configure printer settings
        var printerSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            // For a network printer you can set the URI, e.g.:
            // PrinterUri = new Uri("ipp://printer-server/print"),
            // Or set the printer name directly:
            PrinterName = new PrintDocument().PrinterSettings.PrinterName,

            // Specify duplex mode (Vertical = flip on short edge, Horizontal = flip on long edge)
            Duplex = Aspose.Pdf.Printing.Duplex.Vertical,

            // Define the page range
            PrintRange = Aspose.Pdf.Printing.PrintRange.SomePages,
            FromPage   = fromPage,
            ToPage     = toPage
        };

        // Configure page settings (paper size, margins, etc.)
        var pageSettings = new Aspose.Pdf.Printing.PageSettings
        {
            // Use a standard paper size (A4 in this example)
            PaperSize = Aspose.Pdf.Printing.PaperSizes.A4,

            // Optional: set margins to zero
            Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
        };

        // Use the PdfViewer facade to print the document with the specified settings
        // Note: we reference the class with its fully qualified name to avoid a using directive for Aspose.Pdf.Facades
        using (var viewer = new Aspose.Pdf.Facades.PdfViewer())
        {
            viewer.BindPdf(pdfPath);               // Load the PDF
            viewer.AutoResize = true;              // Adjust size to fit printable area
            viewer.AutoRotate = true;              // Auto‑rotate pages if needed
            viewer.PrintPageDialog = false;        // Suppress the print dialog

            // Print the selected page range with the defined printer and page settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            viewer.Close();                        // Release resources
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}