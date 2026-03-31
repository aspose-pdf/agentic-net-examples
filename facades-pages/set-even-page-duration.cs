using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const double durationSeconds = 5.0; // duration for even pages in seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (i % 2 == 0) // even‑numbered page
                {
                    Page page = doc.Pages[i];
                    page.Duration = durationSeconds;
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Even pages duration set to {durationSeconds} seconds. Saved to '{outputPath}'.");
    }
}