using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "aligned_text.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is left‑aligned.");

            // Align the text to the left margin
            fragment.TextState.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;

            // Position the fragment on the page (optional)
            fragment.Position = new Position(50, 750); // X = 50, Y = 750

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}