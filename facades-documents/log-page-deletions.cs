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

        using (Document doc = new Document(inputPath))
        {
            // Initial page count
            int initialCount = doc.Pages.Count;
            Console.WriteLine($"Initial page count: {initialCount}");

            // Delete a single page (page number 2) if it exists
            if (initialCount >= 2)
            {
                doc.Pages.Delete(2);
                int afterSingleDelete = doc.Pages.Count;
                int removedSingle = initialCount - afterSingleDelete;
                Console.WriteLine($"Deleted page 2. Pages removed: {removedSingle}");
                initialCount = afterSingleDelete;
            }

            // Delete multiple pages using an array (pages 3 and 4 of the current document)
            if (initialCount >= 4)
            {
                int[] pagesToDelete = new int[] { 3, 4 };
                doc.Pages.Delete(pagesToDelete);
                int afterArrayDelete = doc.Pages.Count;
                int removedArray = initialCount - afterArrayDelete;
                Console.WriteLine($"Deleted pages {string.Join(", ", pagesToDelete)}. Pages removed: {removedArray}");
                initialCount = afterArrayDelete;
            }

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
        }
    }
}