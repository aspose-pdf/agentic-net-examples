using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder = "SvgImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Store only SVG images in the specified folder
                    SpecialFolderForSvgImages = svgFolder,
                    // Example raster image handling (optional)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit HtmlSaveOptions
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}'. SVG images stored in '{svgFolder}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}