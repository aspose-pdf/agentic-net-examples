using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string altText    = "A scenic mountain landscape with a lake in the foreground.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page in the document
            foreach (Page page in doc.Pages)
            {
                // Iterate over the image resources on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Set the alternative text for the image
                    img.TrySetAlternativeText(altText, page);
                    // Uncomment the next line to set alt text only for the first image found
                    // break;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alt text to '{outputPath}'.");
    }
}