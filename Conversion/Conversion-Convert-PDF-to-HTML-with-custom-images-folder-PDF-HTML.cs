using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPdfPath = "input.pdf";

        // Desired output HTML file path
        string outputHtmlPath = "output.html";

        // Name of the folder (relative to the HTML file) where images will be stored
        string imagesFolderName = "images";

        // Resolve absolute path for the images folder (kept for possible custom handling)
        string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(outputHtmlPath)) ?? Directory.GetCurrentDirectory();
        string imagesFolderPath = Path.Combine(outputDirectory, imagesFolderName);

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"PDF file not found at '{inputPdfPath}'.");

            // Ensure the images folder exists – even though we are not explicitly assigning it to HtmlSaveOptions,
            // Aspose.Pdf will create the folder automatically if needed.
            Directory.CreateDirectory(imagesFolderPath);

            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate only the content inside the <body> tag
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,

                // Save raster images as external PNG files referenced via SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                // Do not embed images into the HTML; keep them as separate files
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,

                // Produce a single HTML file (no per‑page splitting)
                SplitIntoPages = false
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
            Console.WriteLine($"HTML successfully saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
