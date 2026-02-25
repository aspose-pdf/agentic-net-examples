using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Always wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example operation: display the number of pages
            Console.WriteLine($"Document loaded: {inputPath}");
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Example: iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                Console.WriteLine($"Page {i} size: {page.PageInfo.Width} x {page.PageInfo.Height}");
            }
        }
    }
}