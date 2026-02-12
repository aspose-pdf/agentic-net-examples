using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where the split HTML files will be written
        const string outputDirectory = "output_html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,               // One HTML file per PDF page
                Title = "Page {0}"                  // Optional: set a title pattern
            };

            // Base file name for the generated HTML pages.
            // Aspose.Pdf will append the page number (e.g., page_1.html, page_2.html, …)
            string baseHtmlPath = Path.Combine(outputDirectory, "page.html");

            // Save the document as HTML pages using the configured options
            pdfDocument.Save(baseHtmlPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}