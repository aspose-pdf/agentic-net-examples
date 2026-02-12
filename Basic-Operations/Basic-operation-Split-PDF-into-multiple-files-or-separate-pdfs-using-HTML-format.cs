using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for the generated HTML pages
        const string outputDirectory = "output_html";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true, // one HTML file per PDF page
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                Title = Path.GetFileNameWithoutExtension(inputPdfPath) // optional title for the HTML pages
            };

            // Save the PDF as HTML pages.
            // Aspose.Pdf will create files like output_page1.html, output_page2.html, etc.
            string htmlOutputPath = Path.Combine(outputDirectory, "output.html");
            pdfDocument.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine($"PDF successfully split into HTML pages in folder: {outputDirectory}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
