using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment and Position

class Program
{
    static void Main()
    {
        const string outputPath = "character_spacing_example.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Aspose.Pdf Character Spacing Demo");

            // Configure the TextState of the fragment (TextState is read‑only, modify its members)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            fragment.TextState.CharacterSpacing = 2.0f; // controls inter‑character gaps

            // Set the fragment position on the page
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
