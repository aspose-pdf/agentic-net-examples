using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextFragment, Position, FontRepository

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TitlePage.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Set the document title (metadata)
            doc.SetTitle("Sample PDF with Custom Title Page");

            // Add a new page that will serve as the title page
            Page titlePage = doc.Pages.Add();

            // Create a TextFragment for the title text
            TextFragment titleFragment = new TextFragment("My Custom Title");

            // Position the text (centered roughly)
            titleFragment.Position = new Position(200, 700); // X, Y coordinates

            // Customize font and color
            titleFragment.TextState.Font = FontRepository.FindFont("Helvetica"); // custom font
            titleFragment.TextState.FontSize = 36;                               // custom size
            titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue; // custom color

            // Add the TextFragment to the page's paragraphs collection
            titlePage.Paragraphs.Add(titleFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with title page saved to '{outputPath}'.");
    }
}