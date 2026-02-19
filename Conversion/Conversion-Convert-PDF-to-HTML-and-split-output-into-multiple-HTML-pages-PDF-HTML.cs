using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Folder where the split HTML pages will be saved
        const string outputFolder = "output_html";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create the output folder if it does not exist
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Configure HTML save options to generate one HTML file per PDF page
        HtmlSaveOptions htmlOptions = new HtmlSaveOptions
        {
            // Enable splitting into separate HTML pages
            SplitIntoPages = true,

            // Optional: keep full HTML markup
            HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,

            // Optional: how raster images are saved (embedded as PNG inside SVG)
            RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
        };

        // Save the PDF as HTML pages. When SplitIntoPages is true, the output path is treated as a folder.
        pdfDocument.Save(outputFolder, htmlOptions);

        Console.WriteLine($"Conversion completed. HTML pages are located in '{outputFolder}'.");
    }
}