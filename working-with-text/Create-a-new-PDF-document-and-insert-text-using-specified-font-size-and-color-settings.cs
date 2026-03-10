using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document using the Document constructor
        Document doc = new Document();

        // Add a single page to the document
        Page page = doc.Pages.Add();

        // Create a text fragment with the desired content
        TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");

        // Set the position where the text will appear on the page
        fragment.Position = new Position(100, 700); // X=100, Y=700

        // Configure font, size, and color using the TextState object
        fragment.TextState.Font = FontRepository.FindFont("Helvetica"); // specify font
        fragment.TextState.FontSize = 24;                               // specify size
        fragment.TextState.ForegroundColor = Color.Blue;                // specify color

        // Append the text fragment to the page using TextBuilder
        TextBuilder builder = new TextBuilder(page);
        builder.AppendText(fragment);

        // Save the PDF document
        doc.Save("output.pdf");
    }
}