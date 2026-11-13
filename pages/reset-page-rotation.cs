using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "rotated_input.pdf";
        const string outputPath = "restored_output.pdf";
        const int pageNumber = 1; // 1‑based index of the page to restore

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists (pages are 1‑based – rule: page-indexing-one-based)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Retrieve the page
            Page page = doc.Pages[pageNumber];

            // Reset any rotation applied earlier using the correct Rotate property.
            // The Rotation enum provides None, on90, on180, on270 values.
            page.Rotate = Rotation.None;

            // Save the modified PDF. No SaveOptions needed because we are saving to PDF format.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumber} rotation restored. Output saved to '{outputPath}'.");
    }
}
