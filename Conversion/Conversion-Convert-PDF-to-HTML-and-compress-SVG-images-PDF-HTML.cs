using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output HTML file path (second argument)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToHtmlConverter <input.pdf> <output.html>");
            return;
        }

        string pdfPath = args[0];
        string htmlPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Compress any SVG graphics found during conversion
                CompressSvgGraphicsIfAny = true,

                // Save raster images as external PNG files referenced via SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                // Optional: split each PDF page into a separate HTML file (set to false for a single file)
                SplitIntoPages = false,

                // Optional: set a title for the generated HTML page
                Title = Path.GetFileNameWithoutExtension(pdfPath)
            };

            // Perform the conversion and save the HTML output
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}