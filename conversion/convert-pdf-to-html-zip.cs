using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class PdfToHtmlZipper
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Folder where HTML pages and assets will be generated
        const string htmlOutputFolder = "HtmlOutput";

        // Path of the final ZIP archive
        const string zipPath = "HtmlArchive.zip";

        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure a clean output folder
        if (Directory.Exists(htmlOutputFolder))
            Directory.Delete(htmlOutputFolder, true);
        Directory.CreateDirectory(htmlOutputFolder);

        try
        {
            // Load the PDF document (using the standard Document constructor)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page
                    SplitIntoPages = true,

                    // Optional: embed resources directly into each HTML file
                    // PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: control how raster images are saved
                    // RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Save the PDF as HTML. The file name is used as a prefix for generated pages.
                string baseHtmlPath = Path.Combine(htmlOutputFolder, "document.html");
                pdfDoc.Save(baseHtmlPath, htmlOptions);
            }

            // Create a ZIP archive containing all generated HTML files and assets
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(htmlOutputFolder, zipPath, CompressionLevel.Optimal, false);

            Console.WriteLine($"HTML pages and assets have been zipped to '{zipPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}