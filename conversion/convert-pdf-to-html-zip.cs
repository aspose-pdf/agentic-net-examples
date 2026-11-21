using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";
        const string htmlFolder   = "html_output";
        const string zipPath      = "html_archive.zip";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure a clean output folder
        if (Directory.Exists(htmlFolder))
            Directory.Delete(htmlFolder, true);
        Directory.CreateDirectory(htmlFolder);

        try
        {
            // Load PDF and convert to HTML (one HTML file per page)
            using (Document doc = new Document(pdfPath))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages        = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion requires GDI+ (Windows only)
                doc.Save(htmlFolder, htmlOpts);
            }

            // Create a zip archive containing all generated HTML files and assets
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(htmlFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

            Console.WriteLine($"HTML pages and assets saved to '{htmlFolder}' and zipped as '{zipPath}'.");
        }
        catch (TypeInitializationException)
        {
            // GDI+ not available on non‑Windows platforms
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}