using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "HtmlOutput";
        const string baseHtmlName = "document.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Full path for the base HTML file (Aspose will create a sibling folder for images)
        string baseHtmlPath = Path.Combine(outputFolder, baseHtmlName);

        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // HtmlSaveOptions – use members that exist across supported Aspose.Pdf versions
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true, // one HTML file per page
                    // Store raster images as external PNG files referenced via SVG – this enum member exists in the current library version
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Do not embed images/fonts into the HTML; they will be written to the generated folder
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding
                };

                // Perform the conversion
                doc.Save(baseHtmlPath, htmlOpts);
            }

            // ---------------------------------------------------------------------
            // After conversion, Aspose creates a folder named "document.html_files"
            // next to the generated HTML files. All raster images are placed there.
            // We now reorganise those images into per‑page sub‑folders.
            // ---------------------------------------------------------------------
            string defaultImagesFolder = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(baseHtmlName) + "_files");
            if (!Directory.Exists(defaultImagesFolder))
            {
                Console.WriteLine($"No image folder was created at '{defaultImagesFolder}'. Nothing to reorganise.");
                return;
            }

            // Get all PNG images that Aspose generated (pattern: image_{page}_{index}.png)
            string[] imageFiles = Directory.GetFiles(defaultImagesFolder, "image_*.png");
            Regex pageRegex = new Regex(@"image_(\d+)_\d+\.png", RegexOptions.IgnoreCase);

            foreach (string imagePath in imageFiles)
            {
                string fileName = Path.GetFileName(imagePath);
                Match m = pageRegex.Match(fileName);
                if (m.Success && int.TryParse(m.Groups[1].Value, out int pageNumber))
                {
                    // Create a sub‑folder for the page, e.g. "page_1"
                    string pageFolder = Path.Combine(defaultImagesFolder, $"page_{pageNumber}");
                    Directory.CreateDirectory(pageFolder);

                    string destPath = Path.Combine(pageFolder, fileName);
                    // Move the image; if a file with the same name already exists, overwrite it
                    File.Move(imagePath, destPath, true);
                }
            }

            Console.WriteLine($"PDF successfully converted to HTML. Output folder: {outputFolder}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
