using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Position

class Program
{
    static void Main()
    {
        const string outputPath = "superscript.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Base coordinates for the normal text
            double baseX = 50;   // distance from the left edge
            double baseY = 750;  // distance from the bottom edge (higher Y = higher on page)

            // Normal text fragment
            TextFragment normal = new TextFragment("Normal text ");
            normal.TextState.Font = FontRepository.FindFont("Arial");
            normal.TextState.FontSize = 12;
            // Position the normal text at the baseline
            normal.Position = new Position(baseX, baseY);
            page.Paragraphs.Add(normal);

            // Superscript text fragment – placed slightly to the right and higher than the baseline
            TextFragment superscript = new TextFragment("Superscript");
            superscript.TextState.Font = FontRepository.FindFont("Arial");
            superscript.TextState.FontSize = 8; // typical smaller size for superscript
            // Raise the Y coordinate (positive offset) to simulate superscript
            superscript.Position = new Position(baseX + 80, baseY + 6);
            page.Paragraphs.Add(superscript);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
