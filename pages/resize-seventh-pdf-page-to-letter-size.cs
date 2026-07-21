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
            // Verify the document has at least seven pages
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document contains fewer than 7 pages.");
                return;
            }

            // Retrieve the seventh page (1‑based indexing)
            Page page = doc.Pages[7];

            // Obtain Letter page dimensions
            PageSize letterSize = PageSize.PageLetter;

            // Apply Letter size using SetPageSize
            page.SetPageSize(letterSize.Width, letterSize.Height);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter format and saved as '{outputPath}'.");
    }
}