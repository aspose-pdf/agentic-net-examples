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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment containing the paragraph to format
            TextFragment paragraph = new TextFragment(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
            );

            // Apply line spacing as a multiplier (1.5 = 150% line height)
            paragraph.TextState.LineSpacing = 1.5f;

            // Optional: set basic font properties for readability
            paragraph.TextState.FontSize = 12;
            paragraph.TextState.Font = FontRepository.FindFont("Arial");

            // Simulate a first‑line indent by shifting the whole paragraph to the right.
            // Aspose.Pdf does not expose a direct ParagraphIndent property on TextState.
            paragraph.Position = new Position(70, 700); // X increased to create an indent effect

            // Add the formatted paragraph to the first page
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted paragraph saved to '{outputPath}'.");
    }
}
