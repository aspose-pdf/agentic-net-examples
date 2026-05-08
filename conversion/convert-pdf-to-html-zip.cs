using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Folder that will hold the generated HTML pages and assets
        string htmlOutputFolder = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? "", "html_output");
        Directory.CreateDirectory(htmlOutputFolder);

        // Base HTML file name (Aspose.Pdf will create additional files when SplitIntoPages is true)
        string baseHtmlPath = Path.Combine(htmlOutputFolder, "document.html");

        // Configure HTML conversion options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Generate one HTML file per PDF page
            SplitIntoPages = true,

            // Save raster images as external PNG files referenced via SVG wrappers
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
        };

        // Convert PDF to HTML
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.Save(baseHtmlPath, htmlOptions);
        }

        // Path for the resulting ZIP archive
        string zipPath = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? "", "html_archive.zip");

        // Create ZIP archive containing all HTML pages and assets
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }
        ZipFile.CreateFromDirectory(htmlOutputFolder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

        Console.WriteLine($"HTML pages and assets have been zipped to: {zipPath}");
    }
}