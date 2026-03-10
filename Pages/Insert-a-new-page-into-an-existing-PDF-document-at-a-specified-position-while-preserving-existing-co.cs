using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertPosition = 2; // 1‑based index where the new page will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the desired position.
            // PageCollection.Insert uses 1‑based indexing, so position 2 inserts before the current page 2.
            Page newPage = doc.Pages.Insert(insertPosition);

            // Optional: set a background color or other properties on the new page
            newPage.Background = Aspose.Pdf.Color.LightGray;

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted a new page at position {insertPosition} and saved to '{outputPath}'.");
    }
}