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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfViewer with the loaded document
            PdfViewer viewer = new PdfViewer(doc);

            // Configure printer settings: two copies, default printer
            PrinterSettings printerSettings = new PrinterSettings
            {
                Copies = 2
            };

            // Configure page settings: landscape orientation
            PageSettings pageSettings = new PageSettings
            {
                Landscape = true
                // PaperSize, Margins, etc., can be set here if needed
            };

            // Print the document using the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // Clean up the viewer
            viewer.Close();
        }
    }
}