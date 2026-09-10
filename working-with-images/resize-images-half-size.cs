using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for Image class hierarchy

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all paragraph elements on the page
                for (int j = 1; j <= page.Paragraphs.Count; j++)
                {
                    // Check if the paragraph is an Image
                    if (page.Paragraphs[j] is Image img)
                    {
                        // Scale the image to 50 % of its original size
                        img.ImageScale = 0.5;
                    }
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images have been resized and saved to '{outputPath}'.");
    }
}