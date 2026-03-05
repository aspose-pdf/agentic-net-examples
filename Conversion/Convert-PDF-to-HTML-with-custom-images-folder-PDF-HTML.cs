using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string imagesFolder = "Images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Save all extracted images to the specified folder
                    SpecialFolderForAllImages = imagesFolder,
                    // Store raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Save the document as HTML using the options (lifecycle: save)
                pdfDoc.Save(outputHtml, htmlOptions);
            }

            Console.WriteLine($"HTML saved to '{outputHtml}'. Images stored in '{imagesFolder}'.");
        }
        // HTML conversion relies on GDI+ (Windows only); handle non‑Windows platforms gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}