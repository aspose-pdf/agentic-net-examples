using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "branded_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Set a LightGray background color for each page via Page.Background
                doc.Pages[i].Background = Color.LightGray;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}
