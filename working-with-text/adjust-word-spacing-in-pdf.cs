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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Hello world with extra spacing");

            // Configure the existing TextState instance (property is read‑only)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Blue;
            fragment.TextState.WordSpacing = 2.0f; // increase spacing between words

            // Optional: set the position of the text on the page
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's content
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with adjusted word spacing to '{outputPath}'.");
    }
}