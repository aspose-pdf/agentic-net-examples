using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_output.pdf";

        // Document implements IDisposable – wrap in using for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, styled text!");

            // Define styling via the existing TextState instance (property is read‑only)
            fragment.TextState.FontSize = 18;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2, 0.4, 0.8); // blueish color

            // Optional: position the fragment on the page
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
