using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_a5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: wrap Document in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (PageCollection is 1‑based)
            foreach (Page page in doc.Pages)
            {
                // Resize each page to A5 size
                page.Resize(PageSize.A5);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to A5 and saved to '{outputPath}'.");
    }
}