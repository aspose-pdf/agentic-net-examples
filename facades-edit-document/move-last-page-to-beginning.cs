using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            if (pageCount < 2)
            {
                Console.WriteLine("Document has less than two pages; no reordering needed.");
                doc.Save(outputPath);
                return;
            }

            // Reference to the last page (pages are 1‑based)
            Page lastPage = doc.Pages[pageCount];

            // Insert the last page at the beginning (position 1)
            doc.Pages.Insert(1, lastPage);

            // Delete the original last page, which is now at the end
            doc.Pages.Delete(pageCount + 1);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages reordered. Saved as '{outputPath}'.");
    }
}
