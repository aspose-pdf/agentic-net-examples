using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Initialize the PdfViewer facade
        PdfViewer viewer = new PdfViewer();
        try
        {
            // Load the PDF document
            viewer.BindPdf(pdfPath);

            // Optional settings for better printing results
            viewer.AutoResize = true;      // Adjust size to printable area
            viewer.AutoRotate = true;      // Auto‑rotate pages if needed
            viewer.PrintPageDialog = false; // Suppress page‑range dialog

            // Print using the default printer
            viewer.PrintDocument();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
        finally
        {
            // Release resources
            viewer.Close();
        }
    }
}