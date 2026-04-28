using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdfPath = "input.pdf";

        // Path to the resulting HTML file.
        const string outputHtmlPath = "output.html";

        // Folder where generated SVG images will be stored.
        const string svgImagesFolder = "svg_images";

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the SVG images folder exists (absolute path is safer).
        string svgFolderFullPath = Path.GetFullPath(svgImagesFolder);
        Directory.CreateDirectory(svgFolderFullPath);

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Store SVG images in the specified folder.
                    SpecialFolderForSvgImages = svgFolderFullPath,

                    // Save raster images as external PNG files referenced via SVG.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Save the PDF as HTML using the configured options.
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"Conversion completed. HTML saved to '{outputHtmlPath}'.");
            Console.WriteLine($"SVG images saved to folder '{svgFolderFullPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
