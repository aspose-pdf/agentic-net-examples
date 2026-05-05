using System;
using System.IO;
using System.Drawing.Printing;               // for default printer name
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;                  // PrinterSettings, Duplex, PageSettings, PaperSizes
using Aspose.Pdf.Devices;                    // Margins

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

            // Prepare printer settings
            Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings();

            // Use the system's default printer
            PrintDocument sysPrintDoc = new PrintDocument();
            printerSettings.PrinterName = sysPrintDoc.PrinterSettings.PrinterName;

            // Enable duplex printing (flip on the long edge)
            printerSettings.Duplex = Aspose.Pdf.Printing.Duplex.Vertical; // or Duplex.Horizontal for short‑edge flip

            // Set number of copies and collate them
            printerSettings.Copies = 2;      // example: 2 copies
            printerSettings.Collate = true; // collated copies

            // Optional: define page settings (paper size, margins, etc.)
            Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings
            {
                PaperSize = Aspose.Pdf.Printing.PaperSizes.A4,
                Margins = new Aspose.Pdf.Devices.Margins(0, 0, 0, 0)
            };

            // Print the document with the specified settings
            viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
        }

        Console.WriteLine("Print job submitted.");
    }
}