using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        // UNC path to the network printer (replace "printer" with the actual share name if needed)
        const string printerName = @"\\192.168.1.100\printer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Configure printer settings for the network printer
            var printerSettings = new PrinterSettings
            {
                PrinterName = printerName,
                PrintRange = PrintRange.AllPages // print all pages silently
            };

            // Optional page settings (paper size, margins, etc.)
            var pageSettings = new PageSettings(printerSettings)
            {
                // Example: use A4 size and zero margins
                PaperSize = PaperSizes.A4,
                Margins = new Margins(0, 0, 0, 0)
            };

            // Print the PDF directly from the file path to the specified network printer
            PdfViewer.PrintDocuments(printerSettings, pageSettings, pdfPath);

            Console.WriteLine("Print job sent successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}
