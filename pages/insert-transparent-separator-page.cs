using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Text;                // For any text handling if needed

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Determine where to insert the separator page.
            // Example: insert after the first page (position = 2 because indexing is 1‑based).
            int insertPosition = 2; // change as needed for your sections

            // Insert an empty page at the desired position.
            Page separator = doc.Pages.Insert(insertPosition);

            // Set the page background to transparent.
            // Aspose.Pdf.Color.Transparent provides a fully transparent color.
            separator.Background = Aspose.Pdf.Color.Transparent;

            // Optionally, you can add a small invisible element to ensure the page is kept.
            // Here we add an empty paragraph (no visual effect).
            separator.Paragraphs.Add(new TextFragment(string.Empty));

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added. Saved to '{outputPath}'.");
    }
}