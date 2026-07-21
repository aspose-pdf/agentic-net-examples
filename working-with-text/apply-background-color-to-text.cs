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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Highlighted text");

            // Set the background color on the fragment's TextState BEFORE adding it to the page
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Position the fragment on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page (pages are 1‑based)
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}