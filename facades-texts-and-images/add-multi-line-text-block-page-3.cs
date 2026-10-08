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

        // Load the PDF using the high‑level API (Document)
        Document pdf = new Document(inputPath);

        // Multi‑line text (use '\n' for line breaks)
        string multiLineText = "First line of text\nSecond line of text\nThird line of text";

        // Create a TextFragment and configure its appearance
        TextFragment fragment = new TextFragment(multiLineText)
        {
            // Position the lower‑left corner of the fragment (left margin = 20 points)
            Position = new Position(20, 750) // X = 20, Y = 750 points from bottom
        };

        // Set font, size, color and custom line spacing (leading)
        fragment.TextState.Font = FontRepository.FindFont("Arial");
        fragment.TextState.FontSize = 12;
        fragment.TextState.ForegroundColor = Color.Black;
        // Use LineSpacing instead of the non‑existent Leading property
        fragment.TextState.LineSpacing = 18; // 1.5 × font size (12 pt) = 18 pt

        // Add the fragment to page 3 (pages are 1‑based)
        pdf.Pages[3].Paragraphs.Add(fragment);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}
