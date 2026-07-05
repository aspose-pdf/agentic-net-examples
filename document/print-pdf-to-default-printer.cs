using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Printing;           // For printer‑related types (not a Facades namespace)

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before attempting to print.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the fully qualified name to avoid importing the Aspose.Pdf.Facades namespace.
        var viewer = new Aspose.Pdf.Facades.PdfViewer();

        try
        {
            // Load the PDF document into the viewer.
            viewer.BindPdf(pdfPath);

            // Optional: adjust printing behavior.
            viewer.AutoResize = true;      // Fit the page to the printable area.
            viewer.AutoRotate = true;      // Rotate pages automatically if needed.
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog.

            // Send the document to the default printer.
            viewer.PrintDocument();
        }
        finally
        {
            // Ensure resources are released even if printing fails.
            viewer.Close();
        }

        Console.WriteLine("Print job dispatched successfully.");
    }
}