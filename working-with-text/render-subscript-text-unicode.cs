using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document doc = new Document();
        // Add a blank page
        Page page = doc.Pages.Add();

        // Use a Unicode subscript character (U+2082) to render "H₂O"
        // This avoids the need for a Paragraph or the non‑existent Baseline property.
        TextFragment formulaFragment = new TextFragment("H\u2082O");
        // Adjust the overall font size as needed
        formulaFragment.TextState.FontSize = 12;
        // Optionally set the font (e.g., Helvetica) to ensure the subscript glyph is available
        formulaFragment.TextState.Font = FontRepository.FindFont("Helvetica");

        // Add the fragment to the page
        page.Paragraphs.Add(formulaFragment);

        // Save the resulting PDF
        doc.Save("SubscriptExample.pdf");
        Console.WriteLine("PDF with subscript text saved as 'SubscriptExample.pdf'.");
    }
}
