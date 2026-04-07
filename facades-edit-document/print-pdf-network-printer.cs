using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PrinterSettings printerSettings = new PrinterSettings();
            // Specify the network printer via its IP address (IPP protocol)
            printerSettings.PrinterUri = new Uri("ipp://192.168.1.100/printers/NetworkPrinter");

            // Send the document to the printer silently
            PdfViewer.PrintDocuments(printerSettings, document);
        }

        Console.WriteLine("Print job sent to network printer.");
    }
}