using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string inputPdfPath = "input.pdf";

        // Output HTML file path (base name). When SplitIntoPages is true,
        // Aspose.Pdf will generate separate HTML files for each page
        // (e.g., output_1.html, output_2.html, ...).
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Split each PDF page into a separate HTML file
                SplitIntoPages = true,

                // Optional: generate only the body content (no <html>/<head> wrapper)
                // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

                // Optional: set a title for the generated HTML pages
                Title = "Converted HTML"
            };

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputHtmlPath));
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine("PDF successfully split into HTML pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}