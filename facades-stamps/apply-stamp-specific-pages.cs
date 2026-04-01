using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp (simple text stamp)
            PageNumberStamp stamp = new PageNumberStamp();
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;
            stamp.Opacity = 0.5f;

            // Apply the stamp only to pages 1, 5, and 10 (if they exist)
            int[] targetPages = new int[] { 1, 5, 10 };
            foreach (int pageNumber in targetPages)
            {
                if (pageNumber <= doc.Pages.Count)
                {
                    Page page = doc.Pages[pageNumber];
                    page.AddStamp(stamp);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine("Stamp applied to pages 1, 5, and 10. Saved as " + outputPath);
    }
}
