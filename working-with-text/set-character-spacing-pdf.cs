using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the content to display
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
            // Set the desired character spacing via the existing TextState (read‑only property)
            fragment.TextState.CharacterSpacing = 2.0f; // controls inter‑character gaps

            // Position the fragment on the page (optional)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraphs collection
            page.Paragraphs.Add(fragment);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("character_spacing_example.pdf");
        }

        Console.WriteLine("PDF created with custom character spacing.");
    }
}