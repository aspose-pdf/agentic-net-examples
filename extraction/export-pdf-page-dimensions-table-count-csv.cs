using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvPath  = "pages_info.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF and create the CSV writer
        using (Document doc = new Document(inputPdf))
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            // CSV header
            writer.WriteLine("PageNumber,Width,Height,TableCount");

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions (points)
                double width  = page.PageInfo.Width;
                double height = page.PageInfo.Height;

                // Count Table objects present in the page's paragraphs
                int tableCount = page.Paragraphs.Count(p => p is Table);

                writer.WriteLine($"{i},{width},{height},{tableCount}");
            }
        }

        Console.WriteLine($"CSV file created at '{csvPath}'.");
    }
}