using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, PageCollection, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_page2.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF, delete page 2, and save the result.
        // Document objects must be wrapped in using blocks for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Ensure the document has at least two pages before attempting deletion.
            if (doc.Pages.Count >= 2)
            {
                doc.Pages.Delete(2); // Delete the second page.
            }
            else
            {
                Console.WriteLine("The document has fewer than 2 pages; no deletion performed.");
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 2 removed. Output saved to '{outputPath}'.");
    }
}