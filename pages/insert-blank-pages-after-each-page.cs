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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Store the original page count (Aspose.Pdf uses 1‑based indexing)
                int originalCount = doc.Pages.Count;

                // Insert a blank page after each existing page.
                // Iterate backwards so that inserted pages do not affect the indices of pages yet to process.
                for (int i = originalCount; i >= 1; i--)
                {
                    // Insert a new empty page after the current page (i + 1).
                    // The overload without a Page argument creates a blank page automatically.
                    doc.Pages.Insert(i + 1);
                }

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved with doubled pages to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
