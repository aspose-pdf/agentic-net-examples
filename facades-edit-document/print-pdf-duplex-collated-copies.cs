using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Ensure the PDF file exists before attempting to print
        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfViewer facade and bind the PDF document
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Configure printer settings: enable duplex printing, set collated copies
            PrinterSettings printerSettings = new PrinterSettings
            {
                // Use the default system printer; replace with a specific name if required
                // PrinterName = "YourPrinterName",
                Copies = 2,                 // number of copies to print
                Collate = true,             // collated copies
                Duplex = Duplex.Vertical   // double‑sided printing (flip on long edge)
            };

            // Optional: configure page settings (paper size, margins, etc.)
            PageSettings pageSettings = new PageSettings
            {
                // PaperSize = PaperSize.A4,
                // Margins = new Margins(0, 0, 0, 0)
            };

            // Print the document with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}
