using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfViewer
using Aspose.Pdf.Printing;             // PrinterSettings, PageSettings
using Aspose.Pdf.Devices;               // Margins (optional)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";                     // PDF to print
        const string networkPrinter = @"\\192.168.1.100\printer";    // UNC path to the network printer

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Initialize the viewer and bind the PDF document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(inputPdfPath);

            // Optional: adjust viewer settings for silent printing
            viewer.AutoResize = true;          // fit content to page if needed
            viewer.AutoRotate = true;          // rotate pages automatically
            viewer.PrintPageDialog = false;    // suppress the print dialog

            // Obtain default printer settings and point them to the network printer
            PrinterSettings printerSettings = viewer.GetDefaultPrinterSettings();
            printerSettings.PrinterName = networkPrinter;   // specify the target printer

            // Optional: set number of copies, duplex mode, etc.
            // printerSettings.Copies = 1;
            // printerSettings.Duplex = Duplex.Simplex;

            // Print the document silently using the configured printer settings
            viewer.PrintDocumentWithSettings(printerSettings);

            // Close the viewer (also disposes internal resources)
            viewer.Close();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}