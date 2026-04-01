using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the input file exists – create a simple placeholder PDF if it does not.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found. Creating a placeholder PDF with 4 pages.");
            using (Document placeholder = new Document())
            {
                // Add four blank pages (default A4 size).
                for (int p = 0; p < 4; p++)
                {
                    placeholder.Pages.Add();
                }
                placeholder.Save(inputPath);
            }
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            int maxPages = pageCount;
            if (maxPages > 4)
            {
                maxPages = 4; // Evaluation mode limit
                Console.WriteLine("Evaluation mode: processing only first 4 pages.");
            }

            for (int i = 1; i <= maxPages; i++)
            {
                if (i % 2 == 1) // odd-numbered page
                {
                    Page page = doc.Pages[i];
                    if (page.PageInfo.IsLandscape)
                    {
                        // Retrieve current MediaBox dimensions.
                        Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                        double width = mediaBox.URX - mediaBox.LLX;
                        double height = mediaBox.URY - mediaBox.LLY;

                        // Swap width and height to change orientation to portrait.
                        Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(
                            mediaBox.LLX,
                            mediaBox.LLY,
                            mediaBox.LLX + height,
                            mediaBox.LLY + width);
                        page.MediaBox = newBox;
                        page.PageInfo.IsLandscape = false;
                        Console.WriteLine($"Page {i} orientation changed to portrait.");
                    }
                }
            }

            doc.Save(outputPath);
            Console.WriteLine($"Document saved to {outputPath}");
        }
    }
}
