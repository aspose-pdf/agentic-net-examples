using System;
using System.IO;
using Aspose.Pdf.Facades; // Provides PdfViewer for printing

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists before attempting to print
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Use PdfViewer to send the document to the default printer.
            // PdfViewer implements IDisposable, so we wrap it in a using block.
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(pdfPath);
                // Print to the default printer. Overloads allow specifying PrinterSettings if needed.
                viewer.PrintDocument();
            }

            Console.WriteLine("Print job successfully sent to the default printer.");
        }
        catch (Exception ex)
        {
            // Catch any errors (e.g., printer not installed, access denied, etc.)
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}
