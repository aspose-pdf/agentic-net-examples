using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // For Color if needed (Color is in Aspose.Pdf)

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

        // Load the PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Alternate colors: even pages LightGray, odd pages White
                page.Background = (i % 2 == 0)
                    ? Aspose.Pdf.Color.LightGray   // Light gray background
                    : Aspose.Pdf.Color.White;      // White background
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with alternating page backgrounds to '{outputPath}'.");
    }
}