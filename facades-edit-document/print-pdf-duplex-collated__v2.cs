using System;
using System.IO;
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
        using (Document pdfDocument = new Document(inputPath))
        {
            // Bind the document to the viewer for printing
            PdfViewer viewer = new PdfViewer();
            try
            {
                viewer.BindPdf(pdfDocument);

                // Configure printer settings
                Aspose.Pdf.Printing.PrinterSettings printerSettings = new Aspose.Pdf.Printing.PrinterSettings();
                // Use the default system printer
                System.Drawing.Printing.PrintDocument sysPrintDoc = new System.Drawing.Printing.PrintDocument();
                printerSettings.PrinterName = sysPrintDoc.PrinterSettings.PrinterName;

                // Enable duplex (double‑sided) printing – horizontal flip
                printerSettings.Duplex = Aspose.Pdf.Printing.Duplex.Horizontal;

                // Set number of copies and collate them
                printerSettings.Copies = 2;
                printerSettings.Collate = true;

                // Optional page settings (can be left with defaults)
                Aspose.Pdf.Printing.PageSettings pageSettings = new Aspose.Pdf.Printing.PageSettings();

                // Print the document with the specified settings
                viewer.PrintDocumentWithSettings(pageSettings, printerSettings);
            }
            finally
            {
                viewer.Close();
            }
        }
    }
}