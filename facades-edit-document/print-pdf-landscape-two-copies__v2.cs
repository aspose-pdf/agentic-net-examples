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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure printer settings: two copies
            PrinterSettings printerSettings = new PrinterSettings();
            printerSettings.Copies = 2;

            // Configure page settings: landscape orientation
            PageSettings pageSettings = new PageSettings();
            pageSettings.Landscape = true;

            // Initialize the viewer for printing
            PdfViewer viewer = new PdfViewer();
            try
            {
                viewer.BindPdf(doc);
                viewer.AutoResize = true;   // adjust size to printer page
                viewer.AutoRotate = true;   // rotate pages if needed
                viewer.PrintPageDialog = false; // suppress page dialog

                // Print the document with the specified settings
                PdfViewer.PrintDocuments(printerSettings, pageSettings, doc);
            }
            finally
            {
                viewer.ClosePdfFile();
            }
        }
    }
}