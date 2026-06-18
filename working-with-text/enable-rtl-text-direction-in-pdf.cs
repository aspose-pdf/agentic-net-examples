using System;
using System.IO;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rtl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Set the overall reading order of the document to right‑to‑left
            doc.Direction = Aspose.Pdf.Direction.R2L;

            // Arabic sample text
            string arabicText = "مرحبا بالعالم";

            // Choose a font that supports Arabic characters and embed it
            Aspose.Pdf.Text.Font font = Aspose.Pdf.Text.FontRepository.FindFont("Arial Unicode MS");
            font.IsEmbedded = true;

            // Create a text fragment with the Arabic text
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment(arabicText);
            fragment.TextState.Font = font;
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the fragment on the page (coordinates are in points)
            fragment.Position = new Aspose.Pdf.Text.Position(100, 700);

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with RTL direction to '{outputPath}'.");
    }
}