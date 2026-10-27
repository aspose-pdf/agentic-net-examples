using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Always wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new text fragment
            TextFragment fragment = new TextFragment("Line 1\nLine 2\nLine 3");

            // Set a custom line spacing (e.g., 1.5 times the default)
            fragment.TextState.LineSpacing = 1.5f; // LineSpacing is the correct property

            // Optionally set other text properties
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the fragment on the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line spacing to '{outputPath}'.");
    }
}