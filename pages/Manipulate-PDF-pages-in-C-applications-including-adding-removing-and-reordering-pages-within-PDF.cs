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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // 1. Add a blank page at the end of the document
                doc.Pages.Add();

                // 2. Remove page number 2 (if the document has at least two pages)
                if (doc.Pages.Count >= 2)
                {
                    doc.Pages.Delete(2);
                }

                // 3. Reorder pages: move the original page 4 to position 2
                //    Adjust indices after the previous deletion
                if (doc.Pages.Count >= 4)
                {
                    // Retrieve the page to move (1‑based indexing)
                    Page pageToMove = doc.Pages[4];

                    // Remove it from its current position
                    doc.Pages.Delete(4);

                    // Insert it at the desired new position (index 2)
                    doc.Pages.Insert(2, pageToMove);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}