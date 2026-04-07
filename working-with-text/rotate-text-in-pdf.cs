using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_text.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment text = new TextFragment("Rotated Text Example");

            // Set the position where the text will be placed (X, Y)
            text.Position = new Position(200, 500);

            // Configure font and size
            text.TextState.Font = FontRepository.FindFont("Helvetica");
            text.TextState.FontSize = 24;

            // Rotate the text by 45 degrees using the Rotation property of TextState
            // (TextState does not expose a TextMatrix property; Rotation achieves the same effect)
            text.TextState.Rotation = 45;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(text);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated text saved to '{outputPath}'.");
    }
}