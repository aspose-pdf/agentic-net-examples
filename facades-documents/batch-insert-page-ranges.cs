using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string destinationPath = "merged_output.pdf";

        // Create an empty destination document
        Document destDoc = new Document();

        var sources = new (string Path, int StartPage, int EndPage)[]
        {
            ("source1.pdf", 1, 3),   // insert pages 1‑3 from source1.pdf
            ("source2.pdf", 5, 7),   // insert pages 5‑7 from source2.pdf
            ("source3.pdf", 2, 4)    // insert pages 2‑4 from source3.pdf
        };

        foreach (var src in sources)
        {
            // Validate source file existence
            if (!File.Exists(src.Path))
            {
                Console.Error.WriteLine($"Source file not found: {src.Path}");
                continue;
            }

            // Load the source PDF
            Document srcDoc = new Document(src.Path);

            // Ensure the requested range is within the source document bounds
            int start = Math.Max(1, src.StartPage);
            int end   = Math.Min(src.EndPage, srcDoc.Pages.Count);
            if (start > end)
            {
                Console.Error.WriteLine($"Invalid page range for {src.Path}: {src.StartPage}-{src.EndPage}");
                continue;
            }

            // Append each page in the range to the destination document
            for (int i = start; i <= end; i++)
            {
                // Add creates a copy of the page, leaving the source unchanged
                destDoc.Pages.Add(srcDoc.Pages[i]);
            }
        }

        // Save the merged PDF
        destDoc.Save(destinationPath);
        Console.WriteLine($"All page ranges have been inserted into '{destinationPath}'.");
    }
}
