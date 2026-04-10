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

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Configure printer settings: two copies
        PrinterSettings printerSettings = new PrinterSettings
        {
            Copies = 2
        };

        // Configure page settings: landscape orientation
        PageSettings pageSettings = new PageSettings
        {
            Landscape = true,
            PaperSize = PaperSizes.A4,
            Margins = new Margins(0, 0, 0, 0)
        };

        // Print the PDF using PdfViewer
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);
            viewer.AutoResize = true;   // fit to printable area
            viewer.AutoRotate = true;   // rotate if needed
            viewer.PrintPageDialog = false; // suppress page range dialog

            // Print with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            viewer.Close();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}