using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Define the desired page size (width and height in points)
        // Example: 8.5 x 11 inches = 612 x 792 points
        double pageWidth  = 612; // 8.5 inches * 72 points per inch
        double pageHeight = 792; // 11 inches * 72 points per inch

        // Load the HTML document using HtmlLoadOptions.
        // The PageSizeAdjustmentMode property does not exist in recent Aspose.Pdf versions,
        // so we simply instantiate the options object without setting it.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML into a PDF Document (wrapped in a using block for deterministic disposal)
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Apply the custom page size to every page in the resulting PDF
            foreach (Page page in pdfDocument.Pages)
            {
                page.SetPageSize(pageWidth, pageHeight);
            }

            // Save the document as PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF with custom page size: {pdfPath}");
    }
}
