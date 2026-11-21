using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";
        const string svgFolder = "svg_images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options – use members that exist in the
                // Aspose.Pdf version referenced by the project.
                var htmlOpts = new HtmlSaveOptions
                {
                    // Store raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                    // Do not embed resources into the HTML; keep them as external files
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,

                    // Keep the whole document in a single HTML file
                    SplitIntoPages = false
                };

                // Perform the conversion; on non‑Windows platforms this may throw
                // TypeInitializationException because GDI+ is required.
                doc.Save(htmlPath, htmlOpts);

                Console.WriteLine($"HTML saved to '{htmlPath}'.");
            }

            // After conversion, move the generated SVG files to the user‑specified folder.
            // Aspose.Pdf creates a folder named "<htmlFileName>_files" next to the HTML file.
            string defaultResourcesFolder = Path.Combine(
                Path.GetDirectoryName(htmlPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(htmlPath) + "_files");

            if (Directory.Exists(defaultResourcesFolder))
            {
                foreach (string svgFile in Directory.GetFiles(defaultResourcesFolder, "*.svg"))
                {
                    string destPath = Path.Combine(svgFolder, Path.GetFileName(svgFile));
                    // Overwrite if a file with the same name already exists.
                    if (File.Exists(destPath))
                        File.Delete(destPath);
                    File.Move(svgFile, destPath);
                }

                // Optionally clean up the now‑empty resources folder.
                try { Directory.Delete(defaultResourcesFolder, true); } catch { }

                Console.WriteLine($"Generated SVG images are stored in '{svgFolder}'.");
            }
            else
            {
                Console.WriteLine("No SVG resources folder was created by Aspose.Pdf.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion is Windows‑only (requires GDI+)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
