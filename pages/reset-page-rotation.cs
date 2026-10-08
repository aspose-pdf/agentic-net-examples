using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_back.pdf";
        const int pageNumber = 1; // 1‑based index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Access the page (1‑based indexing)
            Page page = doc.Pages[pageNumber];

            // Reset the page rotation to its original orientation
            page.Rotate = Rotation.None; // equivalent to 0°

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumber} rotation reset. Saved to '{outputPath}'.");
    }
}
