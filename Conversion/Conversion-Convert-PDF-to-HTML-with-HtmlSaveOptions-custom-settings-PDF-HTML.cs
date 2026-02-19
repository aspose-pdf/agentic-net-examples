using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output HTML
        string inputPdfPath = "input.pdf";
        string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Configure custom HTML conversion options
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Keep only the content inside the <body> tag
            HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
            // Generate a separate HTML file for each PDF page
            SplitIntoPages = true,
            // Embed raster images as PNG inside SVG wrappers
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
            // Optional: set a title for the generated HTML
            Title = "Converted HTML"
        };

        // Save the PDF as HTML using the custom options
        pdfDocument.Save(outputHtmlPath, htmlOptions);
    }
}