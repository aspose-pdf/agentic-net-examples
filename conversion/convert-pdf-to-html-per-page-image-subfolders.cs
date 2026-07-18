using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output HTML file path (first page HTML; additional pages will be generated automatically)
        const string outputHtml = "output.html";
        // Root folder where per‑page image subfolders will be created
        const string imagesRoot = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root images directory exists
        Directory.CreateDirectory(imagesRoot);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Generate a separate HTML file for each PDF page
                SplitIntoPages = true,
                // Save raster images as external PNG files referenced via SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
            };

            // Custom strategy to store each page's images in its own subfolder
            htmlOpts.CustomResourceSavingStrategy = resourceInfo =>
            {
                // The converter passes a ResourceSavingInfo; cast to HtmlImageSavingInfo for images
                var imgInfo = resourceInfo as HtmlSaveOptions.HtmlImageSavingInfo;
                if (imgInfo != null)
                {
                    // Create a subfolder named after the HTML page number (matches PDF page number)
                    string pageFolder = Path.Combine(imagesRoot, $"Page_{imgInfo.HtmlHostPageNumber}");
                    Directory.CreateDirectory(pageFolder);

                    // Build the full file path for the image
                    string imagePath = Path.Combine(pageFolder, imgInfo.SupposedFileName);

                    // Write the image data to the file
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        imgInfo.ContentStream.CopyTo(fs);
                    }

                    // Inform the converter that we have handled the saving
                    resourceInfo.CustomProcessingCancelled = true;

                    // The delegate must return a string (the file name). Since we already saved the file,
                    // returning the file name (or an empty string) satisfies the contract.
                    return imgInfo.SupposedFileName;
                }

                // For non‑image resources let the default handling occur.
                return string.Empty;
            };

            // Perform the conversion and save the HTML output
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine("PDF successfully converted to HTML with per‑page image folders.");
    }
}
