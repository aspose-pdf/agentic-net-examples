using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document src = new Document(inputPath))
        {
            int pageCount = src.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                using (Document single = new Document())
                {
                    // Add the i‑th page from the source document
                    single.Pages.Add(src.Pages[i]);
                    string outPath = Path.Combine(outputDir, $"page_{i}.pdf");
                    single.Save(outPath);
                    Console.WriteLine($"Saved page {i} → {outPath}");
                }
            }
        }
    }
}