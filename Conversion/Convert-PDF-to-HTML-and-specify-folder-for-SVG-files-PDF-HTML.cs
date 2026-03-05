using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgFolderPath = "SvgImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF document (lifecycle: create/load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Save all SVG images into the specified folder
                    SpecialFolderForSvgImages = svgFolderPath,

                    // Example settings (optional but common)
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // HTML conversion may require GDI+ (Windows only); handle gracefully
                try
                {
                    // Save the PDF as HTML using the configured options (lifecycle: save)
                    pdfDocument.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                    Console.WriteLine($"SVG images saved in folder: {svgFolderPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows GDI+. Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}