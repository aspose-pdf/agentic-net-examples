using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "subscript_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define the position where the text will be placed
            // (X = 100, Y = 700) – baseline position
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("H₂O"); // example: subscript 2

            // Set basic text state (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply a negative rise to render subscript.
            // The SetTextRise operator changes the text rise for subsequent text.
            // Adding it before the fragment makes the fragment appear lower.
            page.Contents.Add(new SetTextRise(-5));

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Reset the rise back to normal for any following content
            page.Contents.Add(new SetTextRise(0));

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with subscript text saved to '{outputPath}'.");
    }
}