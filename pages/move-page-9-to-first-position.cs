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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Move page 9 to the first position using Insert/Delete (PageCollection has no Move method)
                int sourceIndex = 9;   // 1‑based index of the page to move
                int targetIndex = 1;   // 1‑based index where the page should be placed

                // Retrieve the page to move
                Page pageToMove = doc.Pages[sourceIndex];

                // Insert the page at the target position
                doc.Pages.Insert(targetIndex, pageToMove);

                // Delete the original occurrence of the page (adjusted after insertion)
                int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
                doc.Pages.Delete(deleteIndex);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 9 moved to first position. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
