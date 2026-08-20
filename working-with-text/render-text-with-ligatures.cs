using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ligatures_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Sample text containing explicit ligature characters (ﬁ, ﬂ)
            TextFragment fragment = new TextFragment("Office ﬂower ﬁnal");

            // Configure the existing TextState (read‑only property) of the fragment
            fragment.TextState.Font = FontRepository.FindFont("Times New Roman");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Color.Black;
            // Note: The TextState class in current Aspose.PDF versions does not expose a Ligatures property.
            // Ligatures are rendered automatically when the text contains the Unicode ligature characters.

            // Position the text on the page
            fragment.Position = new Position(100, 700);

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
