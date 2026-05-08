using System;
using System.IO;
using Aspose.Pdf; // Provides Document, Page, Color, etc.

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Alternate colors: even pages LightGray, odd pages White
                if (i % 2 == 0)
                    page.Background = Color.LightGray; // Aspose.Pdf.Color
                else
                    page.Background = Color.White;     // Aspose.Pdf.Color
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with alternating page backgrounds to '{outputPath}'.");
    }
}