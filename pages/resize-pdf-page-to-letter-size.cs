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

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document contains at least seven pages (1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document has fewer than 7 pages.");
                return;
            }

            // Retrieve page 7 and resize it to Letter format
            Page page7 = doc.Pages[7];
            page7.SetPageSize(PageSize.PageLetter.Width, PageSize.PageLetter.Height);

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter and saved as '{outputPath}'.");
    }
}