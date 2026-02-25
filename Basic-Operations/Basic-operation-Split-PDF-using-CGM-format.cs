using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.cgm";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the CGM file (CGM is input‑only, so we use CgmLoadOptions)
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document src = new Document(inputPath, loadOptions))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= src.Pages.Count; i++)
                {
                    using (Document single = new Document())
                    {
                        // Add the current page to a new document
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