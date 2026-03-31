using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const double durationSeconds = 5.0; // each page displayed for 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                page.Duration = durationSeconds;
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}' with {durationSeconds} seconds per page.");
    }
}