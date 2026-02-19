using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options:
            // - Use PNG page background for raster images
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Conversion successful. HTML saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}