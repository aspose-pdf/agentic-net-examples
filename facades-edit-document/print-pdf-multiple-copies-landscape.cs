using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

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

        // Configure printer settings: two copies
        PrinterSettings printerSettings = new PrinterSettings();
        printerSettings.Copies = 2; // two copies

        // Configure page settings: landscape orientation
        PageSettings pageSettings = new PageSettings();
        pageSettings.Landscape = true; // landscape mode

        // Initialize PdfViewer and print
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(pdfPath);
            viewer.AutoResize = true;   // fit to printable area
            viewer.AutoRotate = true;   // auto‑rotate pages if needed
            viewer.PrintPageDialog = false; // suppress page‑range dialog

            // Print with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }
        finally
        {
            viewer.Close(); // ensure resources are released
        }
    }
}