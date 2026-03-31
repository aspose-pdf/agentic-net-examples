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
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("Document has less than 8 pages.");
                return;
            }

            Page page = doc.Pages[8];
            double originalWidth = page.PageInfo.Width;
            double originalHeight = page.PageInfo.Height;

            // Example modification: change the page size
            page.SetPageSize(500.0, 700.0);
            Console.WriteLine("Page size temporarily changed to 500 x 700.");

            // Revert to the original dimensions
            page.SetPageSize(originalWidth, originalHeight);
            Console.WriteLine($"Page size reverted to original {originalWidth} x {originalHeight}.");

            doc.Save(outputPath);
        }

        Console.WriteLine($"Reverted PDF saved to '{outputPath}'.");
    }
}
