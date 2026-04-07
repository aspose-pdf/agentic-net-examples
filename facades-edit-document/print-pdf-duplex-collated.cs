using System;
using System.IO;
using System.Drawing.Printing;
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
            // Optional: set the document's duplex property (used by the print dialog)
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge; // double‑sided printing

            // Create a viewer and bind the document
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(doc);

                // Configure printer settings (Aspose.Pdf.Printing.PrinterSettings)
                Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings();
                // Use the system default printer
                PrintDocument systemPrintDoc = new PrintDocument();
                printerSettings.PrinterName = systemPrintDoc.PrinterSettings.PrinterName;

                // Enable duplex (double‑sided) printing
                printerSettings.Duplex = Aspose.Pdf.Printing.Duplex.Horizontal; // or Vertical

                // Specify collated copies
                printerSettings.Copies = 2;      // number of copies
                printerSettings.Collate = true; // collated output

                // Page settings (optional – can be left with defaults)
                Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings();

                // Print the document with the specified settings
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
                viewer.Close();
            }
        }

        Console.WriteLine("Print job sent successfully.");
    }
}
