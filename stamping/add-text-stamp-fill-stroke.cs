using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a text stamp with fill‑stroke rendering
        using (Document doc = new Document("input.pdf"))
        {
            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp("Bold Outline");

            // Configure the text appearance
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            stamp.TextState.RenderingMode = TextRenderingMode.FillThenStrokeText;

            // Optional: set outline width for a thicker stroke
            stamp.OutlineWidth = 1.0f;

            // Position the stamp on the page (coordinates are in points)
            stamp.XIndent = 100;
            stamp.YIndent = 500;

            // Add the stamp to the first page (page indexing is 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified document
            doc.Save("output.pdf");
        }
    }
}