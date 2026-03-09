using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_floatingbox.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Example: add a floating box to the first page.
            // Page indexing in Aspose.Pdf is 1‑based.
            Page page = doc.Pages[1];

            // Create a FloatingBox with explicit width and height (in points).
            // 1 point = 1/72 inch.
            FloatingBox box = new FloatingBox(200f, 50f);

            // Position the box absolutely on the page.
            // Left and Top are measured from the lower‑left corner of the page.
            box.PositioningMode = ParagraphPositioningMode.Absolute;
            box.Left = 100f;   // X coordinate
            box.Top  = 600f;   // Y coordinate

            // Optional visual settings.
            box.BackgroundColor = Aspose.Pdf.Color.LightGray;
            box.ZIndex = 10;   // Higher ZIndex draws over lower‑index objects.

            // Add some overlay content – a simple text fragment.
            TextFragment overlayText = new TextFragment("Overlay Text");
            overlayText.TextState.FontSize = 14;
            overlayText.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            overlayText.TextState.Font = FontRepository.FindFont("Helvetica"); // optional

            // The FloatingBox holds its own Paragraphs collection.
            box.Paragraphs.Add(overlayText);

            // Insert the floating box into the page's paragraph collection.
            page.Paragraphs.Add(box);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with FloatingBox: {outputPdf}");
    }
}