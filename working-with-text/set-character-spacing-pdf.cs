using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "character_spacing.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text and position
            TextFragment fragment = new TextFragment("Aspose.Pdf Character Spacing Example")
            {
                Position = new Position(50, 750) // X=50, Y=750
            };

            // Modify the existing TextState (the property is read‑only)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            fragment.TextState.CharacterSpacing = 2f; // increase spacing between characters

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
