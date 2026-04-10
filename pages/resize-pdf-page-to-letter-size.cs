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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least seven pages (pages are 1‑based)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Retrieve page 7
            Page page = doc.Pages[7];

            // Obtain Letter page dimensions from the static PageLetter property
            var letterSize = Aspose.Pdf.PageSize.PageLetter;

            // Change the page size to Letter using SetPageSize(width, height)
            page.SetPageSize(letterSize.Width, letterSize.Height);

            // Save the modified PDF (lifecycle rule: save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 has been resized to Letter format and saved as '{outputPath}'.");
    }
}