using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where HTML pages and assets will be generated
        const string htmlOutputDir = "html_output";

        // Path of the final ZIP archive
        const string zipPath = "html_distribution.zip";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure a clean output directory
        if (Directory.Exists(htmlOutputDir))
            Directory.Delete(htmlOutputDir, true);
        Directory.CreateDirectory(htmlOutputDir);

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page
                    SplitIntoPages = true
                };

                // Save the PDF as HTML; the first file name is used as a base name.
                // All generated files (HTML pages, images, CSS, etc.) will be placed
                // in the same directory (htmlOutputDir).
                string baseHtmlPath = Path.Combine(htmlOutputDir, "index.html");
                pdfDocument.Save(baseHtmlPath, htmlOptions);
            }

            // Create a ZIP archive containing the entire HTML output directory
            if (File.Exists(zipPath))
                File.Delete(zipPath);
            ZipFile.CreateFromDirectory(htmlOutputDir, zipPath, CompressionLevel.Optimal, false);

            Console.WriteLine($"HTML conversion completed. Archive created at: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}