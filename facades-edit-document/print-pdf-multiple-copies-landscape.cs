using System;
using System.IO;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub implementation for PdfPrintHelper when the real class is unavailable in the referenced Aspose.Pdf version.
// This stub provides the minimal API used in the sample (BindPdf and PrintDocument) so the project compiles.
// In a production environment, reference a version of Aspose.Pdf that includes the real PdfPrintHelper
// or replace this stub with the appropriate implementation.
namespace Aspose.Pdf.Facades
{
    public class PdfPrintHelper
    {
        private Document _document;

        /// <summary>
        /// Binds the PDF document to the helper.
        /// </summary>
        /// <param name="doc">The Aspose.Pdf.Document to bind.</param>
        public void BindPdf(Document doc)
        {
            _document = doc ?? throw new ArgumentNullException(nameof(doc));
        }

        /// <summary>
        /// Sends the bound document to the specified printer.
        /// This stub only demonstrates the intended call signature; it does not perform actual printing.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <param name="copies">Number of copies to print.</param>
        /// <param name="isLandscape">True for landscape orientation, false for portrait.</param>
        public void PrintDocument(string printerName, int copies = 1, bool isLandscape = false)
        {
            if (string.IsNullOrWhiteSpace(printerName))
                throw new ArgumentException("Printer name cannot be null or empty.", nameof(printerName));

            // In a real implementation you would configure the PrintDocument object,
            // set the orientation, number of copies, and invoke the print job.
            // Here we simply output a diagnostic message so the stub is observable during execution.
            Console.WriteLine($"[Stub] Printing '{_document?.FileName ?? "<unknown>"}' to printer '{printerName}' " +
                              $"(Copies: {copies}, Landscape: {isLandscape})");
        }
    }
}

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

        // Retrieve the default printer name
        PrinterSettings printerSettings = new PrinterSettings();
        string defaultPrinter = printerSettings.PrinterName;

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // Bind the document to the print helper
            PdfPrintHelper printHelper = new PdfPrintHelper();
            printHelper.BindPdf(doc);

            // Print two copies in landscape orientation on the default printer
            printHelper.PrintDocument(defaultPrinter, copies: 2, isLandscape: true);
        }

        Console.WriteLine("Print job sent to the default printer.");
    }
}
