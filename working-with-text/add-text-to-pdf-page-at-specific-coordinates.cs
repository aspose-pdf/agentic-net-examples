using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string text       = "Sample text";
        const double x = 100; // X coordinate (lower‑left corner)
        const double y = 500; // Y coordinate (lower‑left corner)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired string
            TextFragment fragment = new TextFragment(text);

            // Set the exact position on the page
            fragment.Position = new Position(x, y);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}