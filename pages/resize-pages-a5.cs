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
            Console.WriteLine($"File '{inputPath}' not found. Creating a sample PDF.");
            var sampleDoc = new Document();
            // Add three blank pages as a placeholder.
            for (int i = 0; i < 3; i++)
            {
                sampleDoc.Pages.Add();
            }
            sampleDoc.Save(inputPath);
            Console.WriteLine($"Sample PDF saved to '{inputPath}'.");
        }

        using (Document document = new Document(inputPath))
        {
            int pageCount = document.Pages.Count;
            Console.WriteLine("Original page count: " + pageCount);

            // Resize each page to A5 using the PageSize enum.
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                page.Resize(PageSize.A5);
                Console.WriteLine($"Resized page {pageIndex} to A5.");
            }

            document.Save(outputPath);
            Console.WriteLine("Document saved as " + outputPath);
        }
    }
}
