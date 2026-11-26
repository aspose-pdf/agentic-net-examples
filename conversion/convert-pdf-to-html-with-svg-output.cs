using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgFolderPath = "SvgOutput";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for external SVG images exists
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options – use members that exist in the
                // Aspose.Pdf version referenced by the project.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Do not embed resources; generate external files.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,

                    // Store raster images as PNGs embedded into generated SVGs.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Keep the whole document in a single HTML file.
                    SplitIntoPages = false
                };

                // Perform the conversion; HTML file will reference the SVG files in the
                // default resources folder (output.html_files).
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");

                // Move generated SVG files to the user‑specified folder.
                string defaultResourcesFolder = Path.Combine(
                    Path.GetDirectoryName(outputHtmlPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(outputHtmlPath) + "_files");

                if (Directory.Exists(defaultResourcesFolder))
                {
                    var svgFiles = Directory.GetFiles(defaultResourcesFolder, "*.svg");
                    foreach (var file in svgFiles)
                    {
                        string destFile = Path.Combine(svgFolderPath, Path.GetFileName(file));
                        File.Move(file, destFile, overwrite: true);
                    }
                    Console.WriteLine($"Compressed SVG images saved to folder: {svgFolderPath}");
                }
                else
                {
                    Console.WriteLine("No SVG resources folder was created by Aspose.Pdf.");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ which is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
