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

        // Initialize the PdfViewer facade
        using (PdfViewer viewer = new PdfViewer())
        {
            // Load the PDF document
            viewer.BindPdf(pdfPath);

            // Configure viewer for silent printing (no dialogs)
            viewer.AutoResize = true;      // fit to printable area
            viewer.AutoRotate = true;      // auto‑rotate pages if needed
            viewer.PrintPageDialog = false; // suppress the page‑range dialog

            // Set up printer settings for the network printer
            PrinterSettings printerSettings = new PrinterSettings();
            // Use the UNC path to the network printer; replace "printer" with the actual share name
            printerSettings.PrinterName = @"\\192.168.1.100\printer";
            printerSettings.Copies = 1;    // number of copies

            // Send the print job silently
            viewer.PrintDocumentWithSettings(printerSettings);

            // Release resources
            viewer.Close();
        }

        Console.WriteLine("Print job sent to the network printer.");
    }
}