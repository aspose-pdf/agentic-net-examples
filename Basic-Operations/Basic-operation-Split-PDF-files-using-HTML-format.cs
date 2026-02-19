using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfToHtml
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        string inputPdfPath = "input.pdf";

        // Output HTML file path (the base name). When SplitIntoPages is true,
        // Aspose.Pdf will generate one HTML file per PDF page in the same folder.
        string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,                     // Enable per‑page HTML files
                SplitCssIntoPages = false,                 // Keep a single CSS file (optional)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                // Additional options can be set here if required
            };

            // Save the PDF as HTML using the configured options
            // This will create output.html (first page) and additional files like output_2.html, etc.
            pdfDocument.Save(outputHtmlPath, htmlOptions);

            Console.WriteLine("PDF successfully split into HTML pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
