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

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the custom SVG folder exists
        Directory.CreateDirectory(svgFolder);

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Folder where extracted SVG images will be saved
                    SpecialFolderForSvgImages = svgFolder,

                    // Example: save raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                    // Keep all pages in a single HTML file (default)
                    SplitIntoPages = false
                };

                // Save as HTML with the specified options (must pass SaveOptions for non‑PDF output)
                pdfDoc.Save(outputHtml, htmlOpts);

                Console.WriteLine($"HTML saved to '{outputHtml}'. SVG images stored in '{svgFolder}'.");
            }
        }
        // HTML conversion uses GDI+ and may fail on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}