using System;
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
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Hello Aspose PDF!");

            // Set character spacing on the TextState before adding the fragment
            fragment.TextState.CharacterSpacing = 2.0f; // increase spacing between characters

            // Optional styling (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the configured text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}