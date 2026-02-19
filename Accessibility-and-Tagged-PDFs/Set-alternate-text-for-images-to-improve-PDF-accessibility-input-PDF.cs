using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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

                // Iterate over all images on the current page
                foreach (XImage image in page.Resources.Images)
                {
                    // Build a simple alternate text description
                    string altText = $"Image {(string.IsNullOrEmpty(image.Name) ? "Unnamed" : image.Name)} on page {pageNumber}";

                    // Set the alternate text for the image; ignore the boolean result
                    image.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with alternate text: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}