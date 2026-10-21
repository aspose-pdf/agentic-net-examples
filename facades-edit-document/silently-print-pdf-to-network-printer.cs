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
        // Network printer UNC path (IP address based)
        const string printerName = @"\\192.168.1.100\Printer";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF and print silently to the specified network printer
            using (PdfViewer viewer = new PdfViewer())
            {
                // Bind directly from file path – no need to keep a Document instance
                viewer.BindPdf(pdfPath);

                // Configure printer settings for the network printer
                var printerSettings = new PrinterSettings
                {
                    PrinterName = printerName,
                    PrintRange = PrintRange.AllPages // print all pages
                };

                // Page settings – use defaults (you can customise paper size, margins, etc.)
                var pageSettings = new PageSettings(printerSettings);

                // Print the document silently
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            }

            Console.WriteLine("Print job sent successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
