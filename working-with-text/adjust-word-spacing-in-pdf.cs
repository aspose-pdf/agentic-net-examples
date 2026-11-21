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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment containing the text to insert
            TextFragment fragment = new TextFragment("Sample text with custom spacing");

            // Configure the existing TextState (do NOT replace the whole object)
            fragment.TextState.FontSize = 12f;                     // float literal
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            fragment.TextState.WordSpacing = 2.0f;                // increase spacing between words (float literal)

            // Add the fragment to the first page (adjust page index as needed)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
