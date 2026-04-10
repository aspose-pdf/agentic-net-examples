using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For Aspose.Pdf.Color (if needed, but Color is in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "alternating_background.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Alternate background: odd pages LightGray, even pages White
                page.Background = (i % 2 == 1)
                    ? Aspose.Pdf.Color.LightGray   // Light gray background
                    : Aspose.Pdf.Color.White;      // White background
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with alternating page backgrounds to '{outputPath}'.");
    }
}