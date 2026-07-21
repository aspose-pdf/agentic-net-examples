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

        // Initialize the PdfViewer facade and bind the PDF file.
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(pdfPath);
            viewer.AutoResize = true;   // Scale to fit printable area.
            viewer.AutoRotate = true;   // Auto‑rotate pages if needed.
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog.

            // Configure printer settings: two copies.
            PrinterSettings printerSettings = new PrinterSettings();
            printerSettings.Copies = 2; // Number of copies.

            // Configure page settings: landscape orientation.
            PageSettings pageSettings = new PageSettings();
            pageSettings.Landscape = true; // Landscape mode.

            // Print the document using the specified settings.
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }
        finally
        {
            // Release all resources held by the viewer.
            viewer.Close();
        }
    }
}