using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "large.pdf";
        const string outputRoot = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputRoot);

        try
        {
            using (Document src = new Document(inputPdf))
            {
                int pageCount = src.Pages.Count;
                for (int i = 1; i <= pageCount; i++) // 1‑based indexing
                {
                    using (Document single = new Document())
                    {
                        single.Pages.Add(src.Pages[i]);
                        string pageFolder = Path.Combine(outputRoot, $"Page_{i}");
                        Directory.CreateDirectory(pageFolder);
                        string outPath = Path.Combine(pageFolder, $"Page_{i}.pdf");
                        single.Save(outPath); // PDF output (no SaveOptions needed)
                        Console.WriteLine($"Saved page {i} → {outPath}");
                    }
                }
            }
            Console.WriteLine("Batch split completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}