using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "strikethrough.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired content
            TextFragment tf = new TextFragment("This text is struck through.");

            // Enable strikeout via the TextState property
            tf.TextState.StrikeOut = true;

            // Optional: set font and size for better appearance
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}