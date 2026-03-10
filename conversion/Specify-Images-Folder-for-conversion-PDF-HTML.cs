using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string imagesFolderPath = "Images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolderPath);

        try
        {
            // Load PDF document (lifecycle rule: use using)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create HtmlSaveOptions (lifecycle rule: use create)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Specify folder where all extracted images will be saved
                htmlOpts.SpecialFolderForAllImages = imagesFolderPath;

                // Optional: choose how raster images are saved
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg;

                // Save PDF as HTML with the specified options (lifecycle rule: use save)
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"Conversion completed. HTML saved to '{outputHtmlPath}'. Images saved in '{imagesFolderPath}'.");
        }
        // HTML conversion uses GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}