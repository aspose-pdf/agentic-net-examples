using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Folder where external resources (images, fonts, etc.) will be saved
    private static readonly string ResourcesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HtmlResources");

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the resources folder exists
        Directory.CreateDirectory(ResourcesFolder);

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Save raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                    // Assign custom resource‑saving strategy
                    CustomResourceSavingStrategy = new HtmlSaveOptions.ResourceSavingStrategy(CustomResourceSaving)
                };

                // Perform the conversion
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }

    // Custom resource‑saving delegate
    private static string CustomResourceSaving(SaveOptions.ResourceSavingInfo resourceInfo)
    {
        // If the resource is not an image (e.g., a font), let the default converter handle it
        if (!(resourceInfo is HtmlSaveOptions.HtmlImageSavingInfo imageInfo))
        {
            resourceInfo.CustomProcessingCancelled = true; // delegate processing to the built‑in handler
            return string.Empty;
        }

        // Build the full path for the external file
        string targetPath = Path.Combine(ResourcesFolder, imageInfo.SupposedFileName);

        // Write the resource bytes to disk
        using (FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
        {
            imageInfo.ContentStream.CopyTo(fs);
        }

        // Return a URL (relative to the HTML file) that will be used in the generated markup
        return $"HtmlResources/{imageInfo.SupposedFileName}";
    }
}