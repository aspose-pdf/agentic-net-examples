using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";
        const string altText = "Image description for accessibility";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page
            foreach (Page page in doc.Pages)
            {
                // Iterate through each image on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Attempt to set alternative text for the image
                    bool success = img.TrySetAlternativeText(altText, page);
                    if (!success)
                    {
                        // Log if the operation could not be performed (e.g., ambiguous image)
                        Console.WriteLine($"Unable to set alt text for an image on page {page.Number}.");
                    }
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}