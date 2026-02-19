using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPdfPath = "input.pdf";

        // Output folder where HTML files will be created
        string outputFolder = "output_html";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Configure HTML save options to split each PDF page into a separate HTML file
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Enable per‑page HTML files
            SplitIntoPages = true,

            // Optional: control how raster images are saved (embedded as Base64 PNG inside SVG)
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

            // Optional: set a title for the generated HTML pages
            Title = "Converted PDF"
        };

        // Define the main HTML file name (additional per‑page files will be created automatically)
        string mainHtmlPath = Path.Combine(outputFolder, "output.html");

        // Save the PDF as HTML using the configured options
        // This will generate output.html plus output_1.html, output_2.html, ... for each PDF page
        pdfDocument.Save(mainHtmlPath, htmlOptions);
    }
}