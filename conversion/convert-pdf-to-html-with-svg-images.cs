using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string htmlPath = "output.html";        // generated HTML file
        const string imagesFolder = "html_images";    // desired folder for SVG/PNG images

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the target images folder exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML save options – use members that exist in all supported Aspose.Pdf versions
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Do not embed resources; let Aspose create external files (default folder handling)
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Save raster images as external PNG files referenced via SVG (keeps vector quality)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                    // Produce a single HTML file (images will be external)
                    SplitIntoPages = false
                };

                // Save PDF as HTML
                pdfDoc.Save(htmlPath, htmlOpts);
                Console.WriteLine($"HTML saved to '{htmlPath}'.");
            }

            // After conversion Aspose creates a folder named "<htmlFileName>_files" next to the HTML file.
            // Move its contents (SVG, PNG, CSS, etc.) to the user‑specified images folder.
            string defaultImagesFolder = Path.Combine(
                Path.GetDirectoryName(htmlPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(htmlPath) + "_files");

            if (Directory.Exists(defaultImagesFolder))
            {
                foreach (string filePath in Directory.GetFiles(defaultImagesFolder))
                {
                    string destPath = Path.Combine(imagesFolder, Path.GetFileName(filePath));
                    // Overwrite if a file with the same name already exists in the target folder
                    File.Copy(filePath, destPath, overwrite: true);
                }

                // Optionally clean up the temporary folder created by Aspose
                try
                {
                    Directory.Delete(defaultImagesFolder, recursive: true);
                }
                catch (Exception cleanupEx)
                {
                    Console.Error.WriteLine($"Could not delete temporary image folder: {cleanupEx.Message}");
                }

                Console.WriteLine($"Images moved to '{imagesFolder}'.");
            }
            else
            {
                Console.WriteLine("No separate image folder was created by Aspose; all resources are embedded in the HTML.");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}
