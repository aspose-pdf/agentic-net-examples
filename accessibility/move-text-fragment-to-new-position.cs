using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "moved_text.pdf";
        const string searchText = "Sample Text"; // text to move
        const double newX = 100; // new X coordinate (points)
        const double newY = 500; // new Y coordinate (points)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the text fragment containing the target string
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            // Ensure at least one occurrence was found
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"Text \"{searchText}\" not found in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Take the first found fragment and move it to the new position
            TextFragment fragment = absorber.TextFragments[1]; // 1‑based indexing
            fragment.Position = new Position(newX, newY);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with moved text to '{outputPath}'.");
    }
}