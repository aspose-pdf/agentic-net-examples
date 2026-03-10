using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Desired output HTML file path
        const string outputHtmlPath = "output.html";

        // Folder where extracted SVG images will be saved
        const string svgFolderPath = "SvgImages";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the SVG folder exists
        Directory.CreateDirectory(svgFolderPath);

        try
        {
            // Load the PDF document (lifecycle: create/load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize HtmlSaveOptions (must be passed explicitly)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Specify the folder for SVG images
                htmlOptions.SpecialFolderForSvgImages = svgFolderPath;

                // Optional: store raster images as external PNG files referenced via SVG
                htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg;

                // Save the document as HTML with the configured options (lifecycle: save)
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML. SVG images saved in '{svgFolderPath}'.");
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}