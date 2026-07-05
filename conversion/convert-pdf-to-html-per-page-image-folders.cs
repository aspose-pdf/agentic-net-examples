using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Input and output directories can remain const because they are simple literals
    const string inputPdfPath = "input.pdf";
    const string outputHtmlDir = "HtmlOutput";

    // Path.Combine is evaluated at runtime, therefore it must be a static readonly field, not const
    static readonly string imagesRoot = Path.Combine(outputHtmlDir, "Images");

    static void Main()
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directories exist
        Directory.CreateDirectory(outputHtmlDir);
        Directory.CreateDirectory(imagesRoot);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Generate one HTML file per PDF page
                    SplitIntoPages = true,

                    // Save raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                    // Custom strategy to place each page's images into its own subfolder
                    CustomResourceSavingStrategy = resourceInfo =>
                    {
                        // Handle only image resources
                        var imgInfo = resourceInfo as HtmlSaveOptions.HtmlImageSavingInfo;
                        if (imgInfo == null) return null;

                        // Build a subfolder name based on the HTML page number (which matches the PDF page)
                        string pageFolder = Path.Combine(imagesRoot, $"Page_{imgInfo.HtmlHostPageNumber}");
                        Directory.CreateDirectory(pageFolder);

                        // Determine the full file path for the image
                        string filePath = Path.Combine(pageFolder, imgInfo.SupposedFileName);

                        // Write the image bytes to disk
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            imgInfo.ContentStream.CopyTo(fs);
                        }

                        // Return a relative URL that will be inserted into the generated HTML
                        // Use forward slashes to conform to URL format
                        return Path.Combine($"Images/Page_{imgInfo.HtmlHostPageNumber}", imgInfo.SupposedFileName)
                                   .Replace("\\", "/");
                    }
                };

                // Save each page as a separate HTML file inside the output directory
                string htmlBasePath = Path.Combine(outputHtmlDir, "Page.html"); // base name; Aspose will append page numbers
                pdfDoc.Save(htmlBasePath, htmlOpts);
            }

            Console.WriteLine("PDF successfully converted to HTML with per‑page image folders.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
