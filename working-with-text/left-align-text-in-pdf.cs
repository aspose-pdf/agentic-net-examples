using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "aligned_text.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Left aligned text example.");

            // Configure the text appearance
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Align the text to the left margin
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Position the text within a rectangle on the page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);
            tf.Position = new Position(rect.LLX, rect.URY); // top‑left corner of the rectangle

            // Add the fragment to the page's content
            page.Paragraphs.Add(tf);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}