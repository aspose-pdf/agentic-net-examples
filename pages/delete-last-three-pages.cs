using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (using rule: wrap Document in a using block)
        using (Document doc = new Document(inputPath))
        {
            // Delete the last three pages.
            // Page numbers are 1‑based, so we repeatedly delete the current last page.
            for (int i = 0; i < 3 && doc.Pages.Count > 0; i++)
            {
                int lastPageIndex = doc.Pages.Count; // current last page number
                doc.Pages.Delete(lastPageIndex);
            }

            // Save the modified document (using rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last three pages removed. Result saved to '{outputPath}'.");
    }
}