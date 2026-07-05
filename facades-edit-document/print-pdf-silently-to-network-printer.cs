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

        // Initialize the viewer and bind the PDF file
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Configure printer settings for the network printer
            PrinterSettings printerSettings = new PrinterSettings
            {
                // Use the IPP URI of the network printer (replace "MyPrinter" with the actual printer name if needed)
                PrinterUri = new Uri("ipp://192.168.1.100/printers/MyPrinter")
            };

            // Obtain default page settings and optionally adjust them
            PageSettings pageSettings = viewer.GetDefaultPageSettings();
            // Example: set paper size to A4 (optional, defaults to document size)
            pageSettings.PaperSize = PaperSizes.A4;

            // Print silently using the specified printer and page settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // No need to call Close() explicitly; the using block disposes the viewer
        }

        Console.WriteLine("Print job submitted.");
    }
}