using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path (after inserting the blank page)
        const string outputPath = "output.pdf";
        // Desired position for the new blank page (1‑based index)
        // For example, insert as the 3rd page in the document
        const int insertPosition = 3;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Ensure the position is within the valid range (1 .. Pages.Count + 1)
            int maxPosition = doc.Pages.Count + 1;
            if (insertPosition < 1 || insertPosition > maxPosition)
            {
                Console.Error.WriteLine($"Insert position must be between 1 and {maxPosition}.");
                return;
            }

            // Insert an empty page at the specified position.
            // The Insert method returns the newly created Page object (optional to use).
            Page newPage = doc.Pages.Insert(insertPosition);

            // Optionally, set properties on the new blank page (e.g., background color)
            // newPage.Background = Color.LightGray;   // Uncomment if a background is desired

            // Save the modified document. Since we are saving as PDF, no SaveOptions are required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page inserted at position {insertPosition}. Saved to '{outputPath}'.");
    }
}