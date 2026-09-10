using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RenderSubscriptExample
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "subscript_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a normal text fragment for the base text "H"
            TextFragment baseFragment = new TextFragment("H");
            // Optionally set font and size
            baseFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            baseFragment.TextState.FontSize = 48;

            // Create a text fragment for the subscript "2"
            TextFragment subscriptFragment = new TextFragment("2");
            // Set the subscript flag (internally applies a negative rise)
            subscriptFragment.TextState.Subscript = true;
            // Ensure the subscript uses the same font and size as the base text
            subscriptFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            subscriptFragment.TextState.FontSize = 48;

            // Create another normal text fragment for the trailing text "O"
            TextFragment trailingFragment = new TextFragment("O");
            trailingFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            trailingFragment.TextState.FontSize = 48;

            // Add the fragments to the page in sequence
            page.Paragraphs.Add(baseFragment);
            page.Paragraphs.Add(subscriptFragment);
            page.Paragraphs.Add(trailingFragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with subscript text saved to '{outputPath}'.");
    }
}