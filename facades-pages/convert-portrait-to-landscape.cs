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
            for (int pageIndex = 1; pageIndex <= document.Pages.Count; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                double originalWidth = page.PageInfo.Width;
                double originalHeight = page.PageInfo.Height;

                Console.WriteLine("Page " + pageIndex + " original size: " + originalWidth + " x " + originalHeight);

                if (originalHeight > originalWidth)
                {
                    page.SetPageSize(originalHeight, originalWidth);
                    page.PageInfo.IsLandscape = true;
                }

                double newWidth = page.PageInfo.Width;
                double newHeight = page.PageInfo.Height;
                Console.WriteLine("Page " + pageIndex + " new size: " + newWidth + " x " + newHeight);
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Converted PDF saved to '" + outputPath + "'.");
    }
}