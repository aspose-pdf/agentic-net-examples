using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "updatedtitle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf collections are 1‑based.
            // Retrieve the first top‑level bookmark (outline item).
            if (doc.Outlines.Count > 0)
            {
                var bookmark = doc.Outlines[1]; // index 1 = first bookmark
                bookmark.Title = "Preface";
            }
            else
            {
                Console.WriteLine("No bookmarks found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark title updated and saved to '{outputPath}'.");
    }
}