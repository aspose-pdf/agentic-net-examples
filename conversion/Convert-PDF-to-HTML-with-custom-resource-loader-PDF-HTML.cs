using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options with a custom resource saving strategy
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    // Example: save raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                    // Custom handling of external resources (images, fonts, etc.)
                    CustomResourceSavingStrategy = resourceSavingInfo =>
                    {
                        // Determine a folder for saved resources relative to the HTML output
                        string resourcesDir = Path.Combine(Path.GetDirectoryName(outputHtml) ?? "", "resources");
                        Directory.CreateDirectory(resourcesDir);

                        // Use the suggested file name from the converter
                        string fileName = resourceSavingInfo.SupposedFileName;
                        string fullPath = Path.Combine(resourcesDir, fileName);

                        // Write the resource data to disk
                        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            resourceSavingInfo.ContentStream.CopyTo(fs);
                        }

                        // Return a relative URL that will be inserted into the generated HTML
                        return $"resources/{fileName}";
                    }
                };

                // Convert PDF to HTML using the configured options
                pdfDoc.Save(outputHtml, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}