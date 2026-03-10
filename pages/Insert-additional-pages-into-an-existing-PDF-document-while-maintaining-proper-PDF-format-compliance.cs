using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string extraPath = "extra.pdf";   // PDF whose pages will be inserted
        const string outputPath = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(extraPath))
        {
            Console.Error.WriteLine($"Extra PDF not found: {extraPath}");
            return;
        }

        try
        {
            // Load the main document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Insert two blank pages after the first page.
                // PageCollection.Insert uses 1‑based indexing for the insertion position.
                doc.Pages.Insert(2); // New blank page becomes page #2
                doc.Pages.Insert(3); // Another blank page becomes page #3

                // Load the document that provides pages to be inserted.
                using (Document extraDoc = new Document(extraPath))
                {
                    // Insert the first page of extraDoc at position 4 (after the blanks).
                    // Access pages with 1‑based index as required by the API.
                    doc.Pages.Insert(4, extraDoc.Pages[1]);
                }

                // Save the modified PDF (PDF format, no special SaveOptions needed).
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with inserted pages saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}