using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "image_count.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        int imageCount = 0;

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over the image collection; XImageCollection is not a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    imageCount++;
                    // Example: save each image if needed
                    // img.Save($"image_{imageCount}.png");
                }
            }
        }

        // Write the total count to a text file
        File.WriteAllText(outputTxt, $"Extracted images count: {imageCount}");
        Console.WriteLine($"Image count written to {outputTxt}");
    }
}