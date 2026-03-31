using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set display duration (in seconds) equal to the page index
                page.Duration = (double)i;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with page durations to '{outputPath}'.");
    }
}