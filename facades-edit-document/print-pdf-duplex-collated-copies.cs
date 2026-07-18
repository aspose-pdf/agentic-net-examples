using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;
using Aspose.Pdf.Devices; // required for Aspose.Pdf.Devices.Margins

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

        // Initialize PdfViewer and bind the PDF file
        using (PdfViewer viewer = new PdfViewer())
        {
            viewer.BindPdf(pdfPath);

            // Configure printer settings – use Aspose.Pdf.Printing types explicitly
            var printerSettings = new PrinterSettings
            {
                // Enable duplex (double‑sided) printing; choose Vertical or Horizontal as needed
                Duplex = Duplex.Vertical,

                // Number of copies; set Collate to true for collated copies (default is collated, but explicit is clearer)
                Copies = 2,
                Collate = true
            };

            // Optional: configure page settings (paper size, margins, etc.) – also fully qualified
            var pageSettings = new PageSettings
            {
                PaperSize = PaperSizes.A4,
                // Use Aspose.Pdf.Devices.Margins (not System.Drawing.Printing.Margins) to match the expected type
                Margins = new Margins(0, 0, 0, 0)
            };

            // Print the document with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted.");
    }
}
