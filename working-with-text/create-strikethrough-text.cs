using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "strikethrough.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("This text is struck through");

            // Enable the strikeout style via TextState
            tf.TextState.StrikeOut = true;

            // Position the text on the page (optional)
            tf.Position = new Position(100, 700);

            // Add the TextFragment to the page's content
            page.Paragraphs.Add(tf);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}