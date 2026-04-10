using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the corporate brand color (example RGB: 30, 144, 255)
            Color brandColor = Color.FromArgb(30, 144, 255);

            // Apply the background color to each page (Page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = brandColor;   // Set page background color
            }

            // Save the modified PDF (saving without options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page background color to '{outputPath}'.");
    }
}