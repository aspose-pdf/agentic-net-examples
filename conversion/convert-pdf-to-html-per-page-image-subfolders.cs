using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output HTML file (first page). Additional pages will be created automatically.
        const string outputHtmlPath = "output.html";

        // Root folder where per‑page image subfolders will be created
        const string imagesRootFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the root images folder exists
        Directory.CreateDirectory(imagesRootFolder);

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate one HTML file per PDF page
                SplitIntoPages = true,

                // (optional) embed fonts, CSS, etc. as needed
                // PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
            };

            // Custom strategy to store each page's images in its own subfolder
            htmlOptions.CustomResourceSavingStrategy = info =>
            {
                // Process only image resources
                if (info is HtmlSaveOptions.HtmlImageSavingInfo imageInfo)
                {
                    // PDF page number that generated this image (1‑based)
                    int pageNumber = imageInfo.PdfHostPageNumber;

                    // Create a subfolder for this page: Images/Page_1, Images/Page_2, …
                    string pageFolder = Path.Combine(imagesRootFolder, $"Page_{pageNumber}");
                    Directory.CreateDirectory(pageFolder);

                    // Determine a file name for the image
                    string fileName = imageInfo.SupposedFileName;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        // Fallback to a GUID‑based name if the converter didn't suggest one
                        fileName = $"img_{Guid.NewGuid():N}.png";
                    }

                    // Full path on disk where the image will be written
                    string fullPath = Path.Combine(pageFolder, fileName);

                    // Write the image stream to the file
                    using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        imageInfo.ContentStream.CopyTo(fs);
                    }

                    // Return a relative URL that will be placed into the generated HTML.
                    // The path is relative to the location of the HTML files.
                    return Path.Combine("Images", $"Page_{pageNumber}", fileName)
                           .Replace('\\', '/');
                }

                // For non‑image resources let the default converter handling take place
                return null;
            };

            // Perform the conversion
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine("PDF successfully converted to HTML with per‑page image folders.");
    }
}