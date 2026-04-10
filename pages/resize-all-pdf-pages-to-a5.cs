using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Resize each page to A5 dimensions
                page.Resize(PageSize.A5);
            }

            // Save the resized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to A5 and saved to '{outputPath}'.");
    }
}