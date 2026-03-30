using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PrinterSettings printerSettings = new PrinterSettings();
            // Specify the network printer using its IP address (IPP URI)
            printerSettings.PrinterUri = new Uri("ipp://192.168.1.100/printers/Printer");

            // Print the document silently without a dialog
            PdfViewer.PrintDocuments(printerSettings, doc);
        }

        Console.WriteLine("Print job sent to network printer.");
    }
}
