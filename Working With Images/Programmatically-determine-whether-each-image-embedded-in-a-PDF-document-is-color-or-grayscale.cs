using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to analyze
        const string inputPath = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            int imageCounter = 1;

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images defined in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    // Determine the color type of the image
                    ColorType colorType = img.GetColorType();

                    // Output the result
                    Console.WriteLine(
                        $"Image {imageCounter}: Page {page.Number}, ColorType = {colorType}");

                    imageCounter++;
                }
            }
        }
    }
}