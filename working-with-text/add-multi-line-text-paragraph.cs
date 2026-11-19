using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment, TextState, MarginInfo, HorizontalAlignment

class Program
{
    static void Main()
    {
        const string outputPath = "multiline_paragraph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment instance (represents a paragraph of text)
            TextFragment fragment = new TextFragment();

            // Set the paragraph text (multiple lines separated by newline characters)
            fragment.Text = "First line of the paragraph.\nSecond line of the paragraph.\nThird line of the paragraph.";

            // Align the paragraph horizontally to the center of the page
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Define margins (left, top, right, bottom) in points
            fragment.Margin = new MarginInfo(20, 20, 20, 20);

            // Configure text formatting via TextState
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // cross‑platform color

            // Add the fragment (paragraph) to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF document (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
