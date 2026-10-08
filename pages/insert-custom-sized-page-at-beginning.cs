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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at the beginning (Aspose.Pdf uses 1‑based indexing)
            doc.Pages.Insert(1);

            // Get the newly inserted page
            Page emptyPage = doc.Pages[1];

            // Set custom dimensions (200 × 300 points)
            emptyPage.PageInfo.Width  = 200;
            emptyPage.PageInfo.Height = 300;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Empty page inserted and saved to '{outputPath}'.");
    }
}
