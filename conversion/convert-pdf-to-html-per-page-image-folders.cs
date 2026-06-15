using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output folder where HTML pages and image subfolders will be placed
        const string outputFolder = "HtmlOutput";

        // Base name for the generated HTML files (when SplitIntoPages is true,
        // Aspose.Pdf will create files like output.html, output_1.html, etc.)
        const string htmlBaseName = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Generate one HTML file per PDF page
                SplitIntoPages = true,

                // Custom strategy to store each page's resources (images) in its own subfolder
                CustomResourceSavingStrategy = info =>
                {
                    // Process only image resources; other resources fall back to default handling
                    if (info.ResourceType == SaveOptions.NodeLevelResourceType.Image)
                    {
                        // Determine a page number for the image.
                        // Aspose.Pdf older versions expose HtmlPageNumber, but newer versions do not.
                        // We infer the page number from the suggested file name (e.g., "image_1.png").
                        int pageNumber = ExtractPageNumberFromFileName(info.SupposedFileName);
                        if (pageNumber == 0) pageNumber = 1; // fallback to first page if we cannot parse

                        // Create a subfolder for this page (e.g., Page_1, Page_2, ...)
                        string pageFolder = Path.Combine(outputFolder, $"Page_{pageNumber}");
                        Directory.CreateDirectory(pageFolder);

                        // Determine the file name Aspose.Pdf suggests for this image
                        string fileName = info.SupposedFileName;

                        // Full path where the image will be written
                        string imagePath = Path.Combine(pageFolder, fileName);

                        // Write the image bytes to disk
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            info.ContentStream.CopyTo(fs);
                        }

                        // Return a relative URL that the generated HTML will use to reference the image
                        // Use forward slashes to conform to URL format
                        return Path.Combine($"Page_{pageNumber}", fileName).Replace('\\', '/');
                    }

                    // For non‑image resources let the converter handle them (return null)
                    return null;
                }
            };

            // Full path for the first HTML file (the converter will create additional files as needed)
            string htmlOutputPath = Path.Combine(outputFolder, htmlBaseName);

            // Perform the conversion
            pdfDoc.Save(htmlOutputPath, htmlOpts);
        }

        Console.WriteLine("PDF successfully converted to HTML with per‑page image folders.");
    }

    /// <summary>
    /// Tries to extract a page number from a file name that follows the pattern "*_N.ext" where N is the page number.
    /// Returns 0 if no number can be found.
    /// </summary>
    private static int ExtractPageNumberFromFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return 0;

        // Example file names: "image_1.png", "page2_image.jpg", "img_10.jpeg"
        // The regex looks for the last underscore followed by digits before the extension.
        var match = Regex.Match(fileName, @"_(\d+)(?=\.[^.]+$)");
        if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
            return number;

        // Some versions of Aspose may embed the page number at the start, e.g., "1_image.png"
        match = Regex.Match(fileName, "^(\\d+)_");
        if (match.Success && int.TryParse(match.Groups[1].Value, out number))
            return number;

        return 0;
    }
}
