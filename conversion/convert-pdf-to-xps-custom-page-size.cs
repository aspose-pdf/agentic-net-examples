using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, XpsSaveOptions)
using Aspose.Pdf.Printing;    // PageSize class for custom dimensions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output XPS file path
        const string outputXps = "output.xps";

        // Define custom page size (width, height) in points.
        // Example: A4 landscape (842 x 595 points).
        const double customWidth  = 842.0;
        const double customHeight = 595.0;

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, adjust page size, and save as XPS
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply the custom size to every page (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                page.SetPageSize(customWidth, customHeight);
            }

            // Initialize XPS save options (required for non‑PDF output)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS with the specified options
            pdfDoc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXps}");
    }
}