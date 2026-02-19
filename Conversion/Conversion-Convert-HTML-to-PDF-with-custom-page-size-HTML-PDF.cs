using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions
            var loadOptions = new HtmlLoadOptions();
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Define a custom page size (width and height in points)
            // Example: 500 x 700 points (≈6.94 x 9.72 inches)
            double customWidth = 500.0;
            double customHeight = 700.0;

            // Apply the custom size to the first page (all pages inherit this size)
            pdfDocument.Pages[1].SetPageSize(customWidth, customHeight);

            // Save the resulting PDF
            pdfDocument.Save(pdfPath);
            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}