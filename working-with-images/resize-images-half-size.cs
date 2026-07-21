using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all paragraph objects on the page
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // Check if the paragraph is an Image
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Reduce both width and height to 50% of the original
                        img.ImageScale = 0.5;
                    }
                }
            }

            // Save the modified PDF (PDF format is implicit)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}