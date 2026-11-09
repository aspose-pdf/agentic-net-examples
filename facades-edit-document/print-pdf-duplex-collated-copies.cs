using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string printerName = "YourPrinterName"; // replace with the target printer

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to the viewer.
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(inputPath);

                // Configure printer settings – duplex (long‑edge), collated copies.
                var printerSettings = new Aspose.Pdf.Printing.PrinterSettings
                {
                    PrinterName = printerName,
                    Copies = 2,          // number of copies
                    Collate = true,      // collated output
                    Duplex = Duplex.Vertical // long‑edge binding (double‑sided)
                };

                // Page settings – you can customise paper size, margins, etc.
                var pageSettings = new Aspose.Pdf.Printing.PageSettings(printerSettings);

                // Print the document with the specified settings.
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            }

            Console.WriteLine("Print job submitted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
