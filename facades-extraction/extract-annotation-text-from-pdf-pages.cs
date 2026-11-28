using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "Pages10_15.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        var document = new Document(inputPdf);

        // Ensure the requested page range exists.
        int startPage = 10;
        int endPage   = 15;
        if (startPage > document.Pages.Count)
        {
            Console.Error.WriteLine("Start page is beyond the document page count.");
            return;
        }
        endPage = Math.Min(endPage, document.Pages.Count);

        var sb = new StringBuilder();

        // Iterate over the required pages and collect annotation contents.
        for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
        {
            var page = document.Pages[pageNumber]; // Pages collection is 1‑based.
            foreach (var annotation in page.Annotations)
            {
                // Most annotation types expose the Contents property.
                string content = annotation.Contents;
                if (!string.IsNullOrEmpty(content))
                {
                    sb.AppendLine(content);
                }
            }
        }

        // Write the collected annotation texts to the output file.
        File.WriteAllText(outputTxt, sb.ToString());
        Console.WriteLine($"Annotations from pages {startPage}‑{endPage} saved to '{outputTxt}'.");
    }
}
