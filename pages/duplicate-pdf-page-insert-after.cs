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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain a third page to duplicate.");
                return;
            }

            // Source page to be duplicated (page numbers are 1‑based)
            Page sourcePage = doc.Pages[3];

            // Insert a copy of the source page immediately after the original (position 4)
            // The Insert method that takes a Page argument creates a deep copy of the page.
            doc.Pages.Insert(4, sourcePage);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 duplicated and inserted after original. Saved to '{outputPath}'.");
    }
}
