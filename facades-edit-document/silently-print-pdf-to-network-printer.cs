using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfViewer facade and bind the PDF document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Configure viewer for silent printing
            viewer.AutoResize = true;      // fit to printable area
            viewer.AutoRotate = true;      // rotate pages if needed
            viewer.PrintPageDialog = false; // suppress the print dialog

            // Set up printer settings for the network printer
            PrinterSettings printerSettings = new PrinterSettings();
            // Adjust the printer name/URI as required by your environment.
            // Example using a UNC path to a shared printer:
            printerSettings.PrinterName = @"\\192.168.1.100\Printer";

            // Send the document to the printer silently
            viewer.PrintDocumentWithSettings(printerSettings);

            // Close the viewer (optional, as the using block will dispose it)
            viewer.Close();
        }

        Console.WriteLine("Print job dispatched to the network printer.");
    }
}