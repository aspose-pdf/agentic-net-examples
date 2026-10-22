using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ligatures.pdf";

        // Create a PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Text containing characters that can form ligatures (e.g., "ff")
            TextFragment fragment = new TextFragment("office");

            // Aspose.Pdf does not expose a Ligatures property; ligatures are handled automatically.
            // If you need to adjust spacing, you can use CharacterSpacing or WordSpacing instead.
            // fragment.TextState.CharacterSpacing = 0.5; // optional spacing tweak

            // Set a readable font and size (optional but recommended)
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 48;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ligatures saved to '{outputPath}'.");
    }
}
