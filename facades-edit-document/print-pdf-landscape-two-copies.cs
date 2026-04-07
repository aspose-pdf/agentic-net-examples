using System;
using System.IO;
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

        // Configure printer settings: two copies, default printer
        PrinterSettings printerSettings = new PrinterSettings();
        printerSettings.Copies = 2;
        // printerSettings.PrinterName left as default (system default printer)

        // Configure page settings: landscape orientation
        PageSettings pageSettings = new PageSettings();
        pageSettings.Landscape = true;

        // Print the PDF document using the static PrintDocuments method
        PdfViewer.PrintDocuments(printerSettings, pageSettings, inputPath);

        Console.WriteLine("Print job sent to the default printer with 2 copies in landscape mode.");
    }
}
