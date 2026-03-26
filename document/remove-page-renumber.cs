using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageToDelete = 2; // change to the page number you want to remove (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (pageToDelete < 1 || pageToDelete > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageToDelete}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Delete the specified page; remaining pages are automatically renumbered.
            doc.Pages.Delete(pageToDelete);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageToDelete} removed. Result saved to '{outputPath}'.");
    }
}