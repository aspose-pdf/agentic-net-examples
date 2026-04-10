using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Check that the document has at least three pages (pages are 1‑based)
            if (doc.Pages.Count >= 3)
            {
                // Delete the third page
                doc.Pages.Delete(3);
            }
            else
            {
                Console.WriteLine("The document contains fewer than three pages; no deletion performed.");
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Third page removed. Result saved to '{outputPath}'.");
    }
}