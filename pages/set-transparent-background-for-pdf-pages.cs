using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "overlay_input.pdf";
        const string outputPath = "overlay_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Set the document background to transparent (optional, affects whole doc)
            doc.Background = Color.Transparent;

            // Iterate through all pages (1‑based indexing) and set each page background to transparent
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = Color.Transparent;
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent background applied and saved to '{outputPath}'.");
    }
}