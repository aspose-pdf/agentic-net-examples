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

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Directory where HTML pages and assets will be generated
        string htmlOutputDir = Path.Combine(Path.GetDirectoryName(pdfPath) ?? "", "html_output");
        Directory.CreateDirectory(htmlOutputDir);

        // Base HTML file name (Aspose will create additional files when SplitIntoPages = true)
        string baseHtmlPath = Path.Combine(htmlOutputDir, "index.html");

        // Configure HTML save options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Generate one HTML file per PDF page
            SplitIntoPages = true,

            // Example: embed raster images as external PNG files referenced via SVG
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

            // Optional: set a title for the generated HTML pages
            Title = "Converted PDF to HTML"
        };

        // Load the PDF and save as HTML using the configured options
        using (Document pdfDocument = new Document(pdfPath))
        {
            pdfDocument.Save(baseHtmlPath, htmlOptions);
        }

        // Path for the resulting ZIP archive
        string zipPath = Path.Combine(Path.GetDirectoryName(pdfPath) ?? "", "html_archive.zip");

        // If a previous archive exists, delete it
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a ZIP archive containing all files from the HTML output directory
        ZipFile.CreateFromDirectory(htmlOutputDir, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

        Console.WriteLine($"HTML conversion completed. Files are in: {htmlOutputDir}");
        Console.WriteLine($"ZIP archive created at: {zipPath}");
    }
}