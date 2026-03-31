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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            int lastPageIndex = document.Pages.Count;
            if (lastPageIndex > 0)
            {
                Page lastPage = document.Pages[lastPageIndex];
                // Set the size of the last page to A5 using width and height doubles
                lastPage.SetPageSize((double)PageSize.A5.Width, (double)PageSize.A5.Height);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Last page resized to A5 and saved as '{outputPath}'.");
    }
}
