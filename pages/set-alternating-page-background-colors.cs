using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "alternating_background.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Set background colour directly on the Page object (PageInfo does not expose colours)
                if (i % 2 == 1) // odd pages → LightGray
                    page.Background = Color.LightGray;
                else // even pages → White
                    page.Background = Color.White;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with alternating page backgrounds to '{outputPath}'.");
    }
}
