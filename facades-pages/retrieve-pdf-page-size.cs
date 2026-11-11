using System;
using System.IO;
using System.Drawing;               // SizeF for page dimensions
using Aspose.Pdf;                 // Document provides page information

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

        // Load PDF document (implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 2 exists (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("PDF does not contain a second page.");
                return;
            }

            // Retrieve size of page 2 (points)
            var pageInfo = doc.Pages[2].PageInfo;
            SizeF pageSize = new SizeF((float)pageInfo.Width, (float)pageInfo.Height);

            // Log dimensions (points)
            Console.WriteLine($"Page 2 size: Width = {pageSize.Width} pt, Height = {pageSize.Height} pt");
        }
    }
}