using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
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

            // Configure HTML save options for multi‑page output
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // One HTML file per PDF page
                SplitIntoPages = true,

                // Example settings – can be adjusted as needed
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
            };

            // The converter will generate files like "page1.html", "page2.html", etc.
            // The first argument is a dummy file name; the actual files are placed in the output directory.
            string dummyOutputPath = Path.Combine(outputDirectory, "index.html");
            pdfDocument.Save(dummyOutputPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to multi‑page HTML in folder: {outputDirectory}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF to HTML conversion: {ex.Message}");
        }
    }
}