using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Printing;

class Program
{
    static void Main()
    {
        // Path to the PDF file that should be printed
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfViewer implements IDisposable, so wrap it in a using block
        using (PdfViewer viewer = new PdfViewer())
        {
            // Bind the PDF file to the viewer
            viewer.BindPdf(inputPdfPath);

            // Optional: configure printing options
            viewer.AutoResize = true;      // Scale to fit printable area
            viewer.AutoRotate = true;      // Auto‑rotate pages if needed
            viewer.PrintPageDialog = false; // Suppress the page‑range dialog

            // Print the document using the default printer
            viewer.PrintDocument();
        }

        Console.WriteLine("Print job submitted successfully.");
    }
}