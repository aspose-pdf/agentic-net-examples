using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Use a using block for deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment that contains several lines (\n for line breaks)
            TextFragment fragment = new TextFragment("First line.\nSecond line.\nThird line.");

            // Configure the visual appearance of the text
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.FromRgb(0, 0, 0); // black
            fragment.TextState.LineSpacing = 14; // custom leading (line spacing)

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
