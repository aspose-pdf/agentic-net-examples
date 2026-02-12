using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over each page and set alternate text for every image
            int pageNumber = 1;
            foreach (Page page in pdfDocument.Pages)
            {
                // page.Resources.Images implements IEnumerable<XImage>
                foreach (XImage image in page.Resources.Images)
                {
                    // Create a simple descriptive alternate text
                    string altText = $"Image on page {pageNumber}";

                    // Set the alternate text for accessibility
                    image.TrySetAlternativeText(altText, page);
                }

                pageNumber++;
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Successfully saved PDF with alternate text to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
