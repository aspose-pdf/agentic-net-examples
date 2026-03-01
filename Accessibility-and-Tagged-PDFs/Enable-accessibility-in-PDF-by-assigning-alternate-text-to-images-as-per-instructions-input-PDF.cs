using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "accessible_images.pdf";
        const string altText = "Image description for accessibility";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in doc.Pages)
                {
                    // Iterate through all images on the page.
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Assign alternate text to the image.
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}