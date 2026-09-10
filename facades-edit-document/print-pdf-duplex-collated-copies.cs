using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

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

        // Load the PDF and enable duplex mode for the document
        using (Document doc = new Document(pdfPath))
        {
            // PrintDuplex.DuplexFlipLongEdge = double‑sided, flip on the long edge
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge;

            // Configure printer settings: collated copies and duplex printing
            PrinterSettings printerSettings = new PrinterSettings {
                // Use the default system printer
                PrinterName = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName,
                Copies = 2,               // number of copies
                Collate = true,           // collated copies
                Duplex = Duplex.Vertical  // double‑sided printing (short‑edge flip)
            };

            // Use the printer's default page settings
            PageSettings pageSettings = printerSettings.DefaultPageSettings;

            // Print the document using the PdfViewer facade
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(doc);
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
                viewer.Close();
            }
        }

        Console.WriteLine("Print job submitted.");
    }
}