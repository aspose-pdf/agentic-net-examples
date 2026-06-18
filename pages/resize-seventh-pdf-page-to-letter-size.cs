using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 7 pages
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document does not contain a seventh page.");
                return;
            }

            // Get the seventh page (1‑based indexing)
            Page pageSeven = doc.Pages[7];

            // Retrieve the standard Letter page size
            PageSize letterSize = PageSize.PageLetter;

            // Change the page size to Letter using SetPageSize(width, height)
            pageSeven.SetPageSize(letterSize.Width, letterSize.Height);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter format and saved as '{outputPath}'.");
    }
}