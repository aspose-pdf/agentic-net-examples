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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Resize each page to A5 size
                page.Resize(PageSize.A5);
            }

            // Save the modified document (lifecycle rule: explicit save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages resized to A5 and saved to '{outputPath}'.");
    }
}