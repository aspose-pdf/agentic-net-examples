using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TitlePage.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Set the document title (metadata)
            doc.SetTitle("Sample PDF with Title Page");

            // Add a new page for the title
            Page titlePage = doc.Pages.Add();

            // Create a text fragment for the title
            TextFragment titleFragment = new TextFragment("My Custom Title");
            // Set font, size and color
            titleFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            titleFragment.TextState.FontSize = 36;
            titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the title roughly at the center of the page
            // Page size is A4 by default (595 x 842 points)
            titleFragment.Position = new Position(150, 600);

            // Add the text fragment to the page
            titlePage.Paragraphs.Add(titleFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}