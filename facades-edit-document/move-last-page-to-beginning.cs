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

        // Load the document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // If the document has 0 or 1 pages, just copy it to the output path
            if (pageCount <= 1)
            {
                // Ensure the original file is copied unchanged
                File.Copy(inputPath, outputPath, overwrite: true);
                Console.WriteLine("Document has zero or one page; copied without changes.");
                return;
            }

            // Reorder: move the last page to the beginning
            // 1‑based indexing: Pages[pageCount] is the last page
            Page lastPage = doc.Pages[pageCount];
            // Remove the last page from its original position
            doc.Pages.Delete(pageCount);
            // Insert it at position 1 (the beginning)
            doc.Pages.Insert(1, lastPage);

            // Save the reordered document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}
