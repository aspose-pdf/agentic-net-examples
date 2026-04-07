using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Sample text with background");

            // Set the background color via the TextState before adding to the page
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
            // Optional: set foreground color for contrast
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the TextFragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}