using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set the overall reading order of the document to right‑to‑left
            doc.Direction = Aspose.Pdf.Direction.R2L;

            // OPTIONAL: add an Arabic text fragment to demonstrate the RTL direction
            Page page = doc.Pages[1];
            TextFragment arabicFragment = new TextFragment("مرحبا بالعالم"); // "Hello World" in Arabic

            // Choose a font that supports Arabic characters
            Aspose.Pdf.Text.Font arabicFont = Aspose.Pdf.Text.FontRepository.FindFont("Arial Unicode MS");
            arabicFragment.TextState.Font = arabicFont;
            arabicFragment.TextState.FontSize = 14;

            // Position the fragment (adjust coordinates as needed)
            arabicFragment.Position = new Position(500, 800);
            page.Paragraphs.Add(arabicFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL PDF saved to '{outputPath}'.");
    }
}