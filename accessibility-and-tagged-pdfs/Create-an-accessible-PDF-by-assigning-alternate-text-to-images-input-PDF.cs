using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // For ITaggedContent if needed in future

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
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages collection uses 1‑based indexing; foreach abstracts that
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection is not a dictionary; iterate directly
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Set alternative text for each image on the page
                        // The method returns a bool; we ignore it here
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Save the modified PDF
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