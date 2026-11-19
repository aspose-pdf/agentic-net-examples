using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ligatures_output.pdf";

        // Create a new PDF document with deterministic disposal
        using (Document doc = new Document())
        {
            // Add the first page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Text containing characters that form ligatures (e.g., "office")
            TextFragment fragment = new TextFragment("office");

            // Aspose.Pdf does not expose a Ligatures property on TextState.
            // Ligature support is handled internally by the PDF renderer and cannot be toggled via the API.
            // If you need to adjust text rendering, you can use properties such as CharacterSpacing or WordSpacing.
            // Example (optional): fragment.TextState.CharacterSpacing = 0.0;

            // Optional: set font and size
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 24;

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
