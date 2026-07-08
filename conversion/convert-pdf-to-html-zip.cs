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

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Create a temporary folder for the HTML conversion output
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                          "PdfToHtml_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Base name for the generated HTML files (first page will be this name)
        string htmlBaseName = "document.html";
        string htmlOutputPath = Path.Combine(tempFolder, htmlBaseName);

        // Convert PDF to HTML (one HTML file per PDF page)
        using (Document pdfDoc = new Document(pdfPath))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                SplitIntoPages = true, // generate separate HTML files per page
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes
                                            .AsExternalPngFilesReferencedViaSvg,
                // Optional: set a title for the HTML pages
                Title = "Converted PDF Document"
            };

            pdfDoc.Save(htmlOutputPath, htmlOpts);
        }

        // Path for the final ZIP archive
        const string zipPath = "output.zip";

        // Remove existing ZIP if present
        if (File.Exists(zipPath))
            File.Delete(zipPath);

        // Create a ZIP archive containing all HTML pages and their assets
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // Clean up the temporary folder
        Directory.Delete(tempFolder, true);

        Console.WriteLine($"HTML pages and assets have been zipped to: {zipPath}");
    }
}