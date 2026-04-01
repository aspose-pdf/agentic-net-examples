using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                // Add a couple of pages so we have something to work with.
                tempDoc.Pages.Add();
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
                Console.WriteLine($"Created placeholder '{inputPath}' with {tempDoc.Pages.Count} pages.");
            }
        }

        // Load the source PDF.
        using (Document document = new Document(inputPath))
        {
            // Original number of pages (evaluation mode allows a maximum of 4 pages).
            int originalCount = document.Pages.Count;
            int maxPages = Math.Min(originalCount, 4);

            // Insert a blank page after each existing page, iterating backwards.
            for (int i = maxPages; i >= 1; i--)
            {
                document.Pages.Insert(i + 1);
            }

            // Save the modified PDF.
            document.Save(outputPath);
            Console.WriteLine($"Inserted blank pages. New page count: {document.Pages.Count}");
        }
    }
}
