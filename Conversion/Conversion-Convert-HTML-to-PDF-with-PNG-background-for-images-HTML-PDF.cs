using System;
using System.IO;
using Aspose.Pdf;

class HtmlToPdfWithPngBackground
{
    static void Main(string[] args)
    {
        // Input HTML file path (first argument) and output PDF path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: HtmlToPdfWithPngBackground <input.html> <output.pdf>");
            return;
        }

        string htmlPath = args[0];
        string pdfPath = args[1];

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // Load the HTML document using HtmlLoadOptions (default options are sufficient here)
            Document pdfDocument = new Document(htmlPath, new HtmlLoadOptions());

            // Configure HTML save options to embed raster images as a single PNG background per page
            HtmlSaveOptions saveOptions = new HtmlSaveOptions
            {
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };

            // Save the PDF with the specified options
            pdfDocument.Save(pdfPath, saveOptions);

            Console.WriteLine($"HTML successfully converted to PDF with PNG page backgrounds: '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
