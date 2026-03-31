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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Resize each page to A3 dimensions
            for (int i = 1; i <= document.Pages.Count; i++)
            {
                Page page = document.Pages[i];
                page.SetPageSize(PageSize.A3.Width, PageSize.A3.Height);
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Pages resized to A3 and saved to '" + outputPath + "'.");
    }
}
