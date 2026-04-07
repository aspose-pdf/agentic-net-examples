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
            // Add a new blank page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment textFragment = new TextFragment("This text is struck through");

            // Position the text on the page (x = 100, y = 700)
            textFragment.Position = new Position(100, 700);

            // Enable strikeout via the TextState property
            textFragment.TextState.StrikeOut = true;

            // Optional styling (font size and color)
            textFragment.TextState.FontSize = 20;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(textFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with strikethrough text saved to '{outputPath}'.");
    }
}