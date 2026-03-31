using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Printing;

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
            int startPage = 3;
            int endPage = 6;

            if (doc.Pages.Count < endPage)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages. Cannot modify pages {startPage}-{endPage}.");
                return;
            }

            PaperSize letterSize = PaperSizes.Letter;
            for (int i = startPage; i <= endPage; i++)
            {
                Page page = doc.Pages[i];
                page.SetPageSize(letterSize.Width, letterSize.Height);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {3}-{6} set to Letter size and saved to '{outputPath}'.");
    }
}