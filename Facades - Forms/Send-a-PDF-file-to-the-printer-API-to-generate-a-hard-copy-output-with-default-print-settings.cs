using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be printed
        const string pdfPath = "input.pdf";

        // Verify that the file exists before attempting to print
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create a PdfViewer instance (facade for viewing/printing PDFs)
        PdfViewer viewer = new PdfViewer();

        try
        {
            // Load the PDF into the viewer; this also prepares it for printing
            viewer.BindPdf(pdfPath);

            // Optional settings to improve printing of large documents
            viewer.AutoResize = true;      // Scale pages to fit printable area
            viewer.AutoRotate = true;      // Auto‑rotate pages based on orientation
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog

            // Print using the default printer with default settings
            viewer.PrintDocument();
        }
        finally
        {
            // Ensure resources are released even if an exception occurs
            viewer.Close();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}