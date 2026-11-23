using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string outputPath = "count.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // The top‑level bookmarks are stored in the Outlines collection.
            int topLevelBookmarkCount = doc.Outlines.Count;

            // Write the count to a text file.
            File.WriteAllText(outputPath, topLevelBookmarkCount.ToString());
        }

        Console.WriteLine($"Top‑level bookmark count written to '{outputPath}'.");
    }
}