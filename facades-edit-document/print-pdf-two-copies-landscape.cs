using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Configure printer settings: two copies, default printer
        PrinterSettings printerSettings = new PrinterSettings();
        printerSettings.Copies = 2; // two copies

        // Configure page settings: landscape orientation, A4 size, no margins
        PageSettings pageSettings = new PageSettings();
        pageSettings.Landscape = true;                     // landscape orientation
        pageSettings.PaperSize = PaperSizes.A4;            // A4 paper size
        pageSettings.Margins = new Margins(0, 0, 0, 0);    // zero margins

        // Use PdfViewer (Facade) to print the document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);            // load the PDF
            viewer.AutoResize = true;           // fit to printable area
            viewer.AutoRotate = true;           // auto‑rotate pages if needed
            viewer.PrintPageDialog = false;     // suppress page‑range dialog

            // Print with the specified page and printer settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted.");
    }
}