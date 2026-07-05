using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_ligatures.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with sample text that contains ligatures (e.g., "Office")
            TextFragment fragment = new TextFragment("Office")
            {
                // Position the fragment on the page (coordinates are in points)
                Position = new Position(100, 700)
            };

            // Configure the TextState of the fragment – Font and size
            fragment.TextState.Font = FontRepository.FindFont("Times New Roman");
            fragment.TextState.FontSize = 24;
            // NOTE: Aspose.Pdf's TextFragmentState does NOT expose a Ligatures (or Kerning) property.
            // Ligatures are rendered automatically when the selected font supports them; no extra flag is required.

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ligatures (if supported by the font): '{outputPath}'.");
    }
}