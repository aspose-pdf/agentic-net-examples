using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Desired output HTML file (base name). Aspose.Pdf will generate additional files/folders.
        const string htmlPath = "output.html";

        // Path for the final ZIP archive that will contain the HTML pages and all assets.
        const string zipPath = "output_archive.zip";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page.
                    SplitIntoPages = true,

                    // Save raster images as external PNG files referenced via SVG wrappers.
                    // This creates separate image files that will be included in the ZIP.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Convert PDF to HTML. This will create:
                // - output.html (first page)
                // - output_page_2.html, output_page_3.html, ... (additional pages)
                // - a folder named "output_files" containing CSS, images, etc.
                pdfDoc.Save(htmlPath, htmlOptions);
            }

            // Determine the folder that holds the auxiliary assets.
            // Aspose.Pdf creates a folder named "<htmlFileName>_files".
            string assetsFolder = Path.Combine(
                Path.GetDirectoryName(htmlPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(htmlPath) + "_files");

            // Create the ZIP archive.
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Add the main HTML file.
                archive.CreateEntryFromFile(htmlPath, Path.GetFileName(htmlPath));

                // If the assets folder exists, add all its contents preserving relative paths.
                if (Directory.Exists(assetsFolder))
                {
                    foreach (string filePath in Directory.GetFiles(assetsFolder, "*", SearchOption.AllDirectories))
                    {
                        // Compute a path inside the ZIP that mirrors the folder structure.
                        string relativePath = Path.GetRelativePath(
                            Path.GetDirectoryName(htmlPath) ?? string.Empty,
                            filePath);

                        archive.CreateEntryFromFile(filePath, relativePath);
                    }
                }
            }

            Console.WriteLine($"HTML conversion completed. Archive created at '{zipPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}