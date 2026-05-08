using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page to duplicate.");
                return;
            }

            // Duplicate page 3 and insert the copy immediately after it.
            // Page indices are 1‑based, so the new page should be inserted at position 4.
            // Insert(int, Page) creates a copy of the supplied page.
            doc.Pages.Insert(4, doc.Pages[3]);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 duplicated and inserted after original. Saved to '{outputPath}'.");
    }
}