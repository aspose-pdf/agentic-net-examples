using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            // Create a one‑page portrait PDF (A4 size) as a placeholder.
            using (Document placeholder = new Document())
            {
                // A4 portrait dimensions: 595 x 842 points.
                placeholder.Pages.Add();
                placeholder.Pages[1].SetPageSize(842, 595);
                placeholder.Save(inputPath);
            }
        }

        using (Document document = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                // Retrieve current MediaBox dimensions.
                Rectangle mediaBox = page.MediaBox;
                double width = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // Swap width and height to turn portrait into landscape.
                page.SetPageSize(height, width);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Pages converted to landscape and saved to {outputPath}");
    }
}
