using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "alternating_background.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Alternate background: odd pages LightGray, even pages White
                if (i % 2 == 1)
                    page.Background = Color.LightGray;
                else
                    page.Background = Color.White;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with alternating page backgrounds to '{outputPath}'.");
    }
}