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
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        // Load the HTML document using HtmlLoadOptions.
        // HtmlLoadOptions is the appropriate load options class for HTML input.
        using (Document pdfDoc = new Document(htmlPath, new HtmlLoadOptions()))
        {
            // Define a custom page size (width x height in points).
            // Example: A5 size (420 pt × 595 pt) – you can adjust these values as needed.
            const double customWidth  = 420.0; // points
            const double customHeight = 595.0; // points

            // Apply the custom size to every page in the newly created PDF.
            foreach (Page page in pdfDoc.Pages)
            {
                page.SetPageSize(customWidth, customHeight);
            }

            // Save the result as a PDF file.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF with custom page size: {pdfPath}");
    }
}