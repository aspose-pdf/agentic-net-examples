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
            // Load the PDF document inside a using block (ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Verify that the document has at least 9 pages
                if (doc.Pages.Count < 9)
                {
                    Console.Error.WriteLine("The document contains fewer than 9 pages.");
                    return;
                }

                // Move page 9 to the first position using Insert/Delete (PageCollection has no Move method)
                int sourceIndex = 9; // 1‑based index of the page to move
                int targetIndex = 1; // 1‑based index where the page should be placed

                // Keep a reference to the page that will be moved
                Page pageToMove = doc.Pages[sourceIndex];

                // Insert the page at the target position
                doc.Pages.Insert(targetIndex, pageToMove);

                // Delete the original occurrence of the page (adjusted index after insertion)
                int deleteIndex = sourceIndex >= targetIndex ? sourceIndex + 1 : sourceIndex;
                doc.Pages.Delete(deleteIndex);

                // Save the modified document (PDF format is the default)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 9 moved to first position and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
