using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

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

            // Iterate over all pages in the document
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // If the page has no resources or images, skip it
                if (page.Resources == null || page.Resources.Images == null)
                    continue;

                // Iterate over each image XObject on the page
                foreach (XImage image in page.Resources.Images)
                {
                    // Example alternate text – you can customize this per image
                    string altText = $"Image on page {pageIndex}";

                    // Set the alternate text for the image on the current page
                    // This improves accessibility for screen readers
                    image.TrySetAlternativeText(altText, page);
                }
            }

            // Save the modified PDF using the prescribed rule
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Successfully added alternate text and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}