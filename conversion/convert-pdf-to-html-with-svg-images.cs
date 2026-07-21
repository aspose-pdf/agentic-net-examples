using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string svgImagesFolder = "SvgImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgImagesFolder);

        try
        {
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Save raster images as external PNG files referenced via SVG
                htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg;

                // Store generated SVG images in the specified folder
                htmlOptions.SpecialFolderForSvgImages = svgImagesFolder;

                // Embed all other resources (CSS, fonts, etc.) into the HTML file
                htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Perform the conversion
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            Console.WriteLine($"SVG images saved in folder: {svgImagesFolder}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows GDI+. Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}