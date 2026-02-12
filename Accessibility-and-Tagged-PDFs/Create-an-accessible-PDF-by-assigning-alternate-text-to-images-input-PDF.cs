using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over all pages
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Each page may contain a collection of XImage objects
                foreach (XImage image in page.Resources.Images)
                {
                    // Generate a simple alternate text; you can replace this with a more meaningful description
                    string altText = $"Image on page {pageIndex}, name: {image.Name ?? "Unnamed"}";

                    // Assign the alternate text to the image on the current page
                    // The method returns true if the operation succeeded
                    bool success = image.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        Console.WriteLine($"Warning: Could not set alternate text for image '{image.Name}' on page {pageIndex}.");
                    }
                }
            }

            // Save the modified PDF (using the provided document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Accessible PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}