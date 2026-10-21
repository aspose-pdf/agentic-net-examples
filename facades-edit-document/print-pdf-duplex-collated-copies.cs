using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices; // for Margins if needed

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string printerName = "YourPrinterName"; // replace with actual printer name
        const int copies = 2; // number of collated copies

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize PdfViewer and bind the PDF file
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Configure printer settings: duplex, collated copies, printer name, number of copies
            PrinterSettings printerSettings = new PrinterSettings
            {
                PrinterName = printerName,
                Copies = (short)copies,
                Collate = true,
                // Short‑edge (horizontal) binding for double‑sided printing
                Duplex = Duplex.Horizontal
            };

            // PageSettings can be created from the printer settings; additional page options can be set here
            PageSettings pageSettings = new PageSettings(printerSettings);
            // Example: keep default paper size and zero margins (optional)
            pageSettings.Margins = new Margins(0, 0, 0, 0);

            // Print the document using the specified settings (duplex + collated copies)
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted with duplex mode and collated copies.");
    }
}
