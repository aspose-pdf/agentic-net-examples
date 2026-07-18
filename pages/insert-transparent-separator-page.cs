using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Determine where to insert the separator page.
            // Example: insert after the first page (position 2, because indexing is 1‑based).
            int insertPosition = 2;

            // Insert an empty page at the desired position.
            Page separator = doc.Pages.Insert(insertPosition);

            // Set the page background to transparent.
            separator.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added. Saved to '{outputPath}'.");
    }
}