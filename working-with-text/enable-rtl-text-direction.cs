using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // TextFragment, TextState, FontRepository, Position

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_rtl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the overall reading order of the document to right‑to‑left
            // (Arabic, Hebrew, etc.). This property is on Document, not on TextState.
            doc.Direction = Aspose.Pdf.Direction.R2L;

            // OPTIONAL: add a new Arabic text fragment to demonstrate RTL rendering
            Page firstPage = doc.Pages[1];
            TextFragment arabicFragment = new TextFragment("مرحبا بالعالم"); // "Hello World" in Arabic
            arabicFragment.TextState.Font = FontRepository.FindFont("Arial"); // Choose a font that supports Arabic
            arabicFragment.TextState.FontSize = 14;
            arabicFragment.Position = new Position(100, 700); // Position on the page
            firstPage.Paragraphs.Add(arabicFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL PDF saved to '{outputPath}'.");
    }
}