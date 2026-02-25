using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document src = new Document(inputPdf))
            {
                // Pages are 1‑based; iterate from 1 to Count inclusive
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    using (Document single = new Document())
                    {
                        // Add the i‑th page to the new document
                        single.Pages.Add(src.Pages[i]);
                        string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                        single.Save(outPath);
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }
            Console.WriteLine("Splitting complete.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}