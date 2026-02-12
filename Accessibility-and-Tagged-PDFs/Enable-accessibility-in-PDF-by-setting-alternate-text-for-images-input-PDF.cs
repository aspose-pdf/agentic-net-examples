using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];

                // The Images collection holds all XImage objects used on the page
                var images = page.Resources.Images;
                if (images == null) continue;

                int imageIndex = 0;
                foreach (XImage xImage in images)
                {
                    imageIndex++;

                    // Create a simple alternate text; in real scenarios this could be
                    // derived from metadata, OCR, or supplied by the user.
                    string altText = $"Image on page {pageNumber}, index {imageIndex}";

                    // Set the alternate text for the image on the current page.
                    // TrySetAlternativeText returns a bool indicating success.
                    bool success = xImage.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine($"Warning: Could not set alt text for image {imageIndex} on page {pageNumber}.");
                    }
                }
            }

            // Save the modified PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Successfully saved accessible PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}