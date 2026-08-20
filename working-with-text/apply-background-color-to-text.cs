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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment and set its background color via TextState
            TextFragment fragment = new TextFragment("Hello, world with background!");
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow; // apply background

            // Position the fragment (optional – here we place it at coordinates 100, 700)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with background-colored text to '{outputPath}'.");
    }
}