using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // For each page, merge all existing layers into a single layer.
            // Merging layers makes their content visible as a single layer.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.MergeLayers("VisibleLayer");
            }

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with controlled layer visibility saved to '{outputPath}'.");
    }
}