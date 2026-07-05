using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "title_page.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Set the document's title metadata
            doc.SetTitle("Sample Title Page");

            // Add a new page that will serve as the title page
            Page titlePage = doc.Pages.Add();

            // Create a text fragment for the title
            TextFragment title = new TextFragment("My Custom Title");

            // Configure font, size, and color using TextState
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.FontSize = 36;
            title.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the text near the top and center it horizontally
            title.Position = new Position(0, 800); // X = 0 centers horizontally
            title.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the text fragment to the page
            titlePage.Paragraphs.Add(title);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}