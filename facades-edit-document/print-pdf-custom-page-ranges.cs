using System;
using System.Drawing.Printing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Obtain the default printer name from a System.Drawing.PrintDocument instance
        PrintDocument sysPrintDoc = new PrintDocument();
        string defaultPrinter = sysPrintDoc.PrinterSettings.PrinterName;

        // Prepare Aspose printer settings (first range: pages 1‑5)
        Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings
        {
            PrinterName = defaultPrinter,
            PrintRange = Aspose.Pdf.Printing.PrintRange.SomePages, // fully qualified to avoid ambiguity
            FromPage = 1,
            ToPage = 5,
            Copies = 1
        };

        // Prepare Aspose page settings (A4 size, no margins)
        Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings
        {
            PaperSize = Aspose.Pdf.Printing.PaperSizes.A4,
            Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
        };

        // Use PdfViewer to bind and print the document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);
            viewer.AutoResize = true;   // fit content to printable area
            viewer.AutoRotate = true;   // rotate pages if needed
            viewer.PrintPageDialog = false; // suppress page dialog

            // Print first range (1‑5)
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);

            // Adjust printer settings for second range (8‑10)
            printerSettings.FromPage = 8;
            printerSettings.ToPage = 10;

            // Print second range (8‑10)
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Printing completed for ranges 1‑5 and 8‑10.");
    }
}
