using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;

            // Ensure there are at least three pages to delete
            if (pageCount < 3)
            {
                Console.WriteLine("Document has fewer than three pages; nothing to delete.");
                doc.Save(outputPath);
                return;
            }

            // Delete the last three pages.
            // Page numbers are 1‑based, so start from the current count and work backwards.
            for (int i = pageCount; i > pageCount - 3; i--)
            {
                doc.Pages.Delete(i);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Last three pages removed. Output saved to '{outputPath}'.");
    }
}