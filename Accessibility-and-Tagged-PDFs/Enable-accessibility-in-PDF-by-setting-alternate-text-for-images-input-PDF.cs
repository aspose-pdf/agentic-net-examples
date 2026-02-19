using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input and output PDFs
        string inputPath = "input.pdf";
        string outputPath = "output_accessible.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Iterate over each page and set alternate text for every image
            foreach (Page page in pdfDocument.Pages)
            {
                // Ensure the page has an image collection
                if (page.Resources?.Images != null)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Create a simple description; use the image name if available
                        string altText = !string.IsNullOrEmpty(img.Name)
                            ? $"Image: {img.Name}"
                            : "Image description";

                        // Set the alternate text for accessibility
                        img.TrySetAlternativeText(altText, page);
                    }
                }
            }

            // Save the modified PDF (using the prescribed save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}