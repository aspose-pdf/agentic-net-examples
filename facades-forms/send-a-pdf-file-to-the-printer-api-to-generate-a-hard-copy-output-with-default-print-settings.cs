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
            // Load the PDF file into the viewer
            viewer.BindPdf(pdfPath);

            // Optional: improve printing layout
            viewer.AutoResize = true;   // scale to fit printable area
            viewer.AutoRotate = true;   // rotate pages if needed
            viewer.PrintPageDialog = false; // suppress page range dialog

            // Send the document to the default printer with default settings
            viewer.PrintDocument();
        }
        finally
        {
            // Ensure resources are released
            viewer.Close();
        }

        Console.WriteLine("Print job dispatched successfully.");
    }
}