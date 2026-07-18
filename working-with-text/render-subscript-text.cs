using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RenderSubscript
{
    static void Main()
    {
        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Normal text "H"
            TextFragment tfH = new TextFragment("H");
            // Position the normal text (baseline)
            tfH.Position = new Position(80, 720);
            page.Paragraphs.Add(tfH);

            // Subscript text "2"
            TextFragment tfSub = new TextFragment("2");
            // Set subscript via TextState (internally uses a negative rise)
            tfSub.TextState.Subscript = true;
            // Position the subscript slightly lower than the baseline
            tfSub.Position = new Position(100, 700);
            page.Paragraphs.Add(tfSub);

            // Normal text "O"
            TextFragment tfO = new TextFragment("O");
            tfO.Position = new Position(120, 720);
            page.Paragraphs.Add(tfO);

            // Save the PDF (PDF format, no SaveOptions needed)
            doc.Save("SubscriptOutput.pdf");
        }

        Console.WriteLine("PDF with subscript text saved as 'SubscriptOutput.pdf'.");
    }
}