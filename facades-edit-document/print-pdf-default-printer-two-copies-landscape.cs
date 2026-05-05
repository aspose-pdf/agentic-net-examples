using System;
using System.IO;
using System.Drawing.Printing; // for default printer name
using Aspose.Pdf.Facades;      // PdfViewer
using Aspose.Pdf.Printing;     // PrinterSettings, PageSettings

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

        // Obtain the default printer name from System.Drawing.Printing
        var sysPrintDoc = new PrintDocument();

        // Configure Aspose.Pdf printer settings
        Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            PrinterName = sysPrintDoc.PrinterSettings.PrinterName, // default printer
            Copies = 2                                            // two copies
        };

        // Configure page settings (landscape orientation)
        Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings
        {
            Landscape = true // print in landscape mode
            // PaperSize defaults to the document's first page size; can be set explicitly if needed
        };

        // Use PdfViewer to print the document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);          // load the PDF
            viewer.AutoResize = true;         // fit to printable area
            viewer.AutoRotate = true;         // rotate if needed
            viewer.PrintPageDialog = false;   // suppress page dialog

            // Print with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            viewer.Close(); // release resources
        }

        Console.WriteLine("Print job submitted.");
    }
}