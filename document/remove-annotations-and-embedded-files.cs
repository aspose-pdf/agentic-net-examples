using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // ---------- Remove all annotations ----------
            // Annotations are stored per page. Iterate each page and delete
            // all entries from its Annotations collection (1‑based indexing).
            foreach (Page page in doc.Pages)
            {
                while (page.Annotations.Count > 0)
                {
                    // Delete the last annotation repeatedly until none remain.
                    page.Annotations.Delete(page.Annotations.Count);
                }
            }

            // ---------- Remove all embedded file attachments ----------
            // The EmbeddedFiles collection provides a Delete() method that
            // removes every attachment safely (no Clear() method exists).
            doc.EmbeddedFiles?.Delete();

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF cleaned and saved to '{outputPath}'.");
    }
}