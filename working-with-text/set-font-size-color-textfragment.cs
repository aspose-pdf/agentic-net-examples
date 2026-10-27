using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_text.pdf";

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, styled text!");

            // Modify the existing TextState (the property is read‑only)
            fragment.TextState.FontSize = 24; // Font size in points
            fragment.TextState.ForegroundColor = Color.FromRgb(0.2, 0.4, 0.8); // RGB values (0‑1 range)
            fragment.TextState.Font = FontRepository.FindFont("Arial"); // Optional specific font

            // Add the styled fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
