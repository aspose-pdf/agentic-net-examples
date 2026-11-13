using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "overlay.pdf";
        const string outputPath = "overlay_transparent.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set the page background to fully transparent via Page.Background
                page.Background = Color.Transparent;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent background PDF saved to '{outputPath}'.");
    }
}
