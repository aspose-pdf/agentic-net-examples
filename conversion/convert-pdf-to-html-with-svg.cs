using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class PdfToHtmlConverter
{
    /// <summary>
    /// Converts a PDF document to HTML and saves any generated SVG images to the specified folder.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="htmlPath">Full path where the resulting HTML file will be saved.</param>
    /// <param name="svgFolderPath">Folder where SVG images extracted during conversion will be stored.</param>
    public static void Convert(string pdfPath, string htmlPath, string svgFolderPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolderPath);

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure HTML save options
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                // Store generated SVG images in the provided folder
                SpecialFolderForSvgImages = svgFolderPath,

                // Optional: keep the layout fixed (change to false for flow layout)
                FixedLayout = true
            };

            // Save the document as HTML using the configured options
            pdfDocument.Save(htmlPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        Console.WriteLine($"SVG images saved to folder: {svgFolderPath}");
    }

    // Example usage
    static void Main()
    {
        string inputPdf   = @"C:\Data\sample.pdf";
        string outputHtml = @"C:\Data\output.html";
        string svgFolder  = @"C:\Data\SvgImages";

        Convert(inputPdf, outputHtml, svgFolder);
    }
}