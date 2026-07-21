using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "strikethrough_output.pdf";

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a blank page (first page will be at index 1)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Strikethrough Example");

            // Enable strikeout via the TextState property
            fragment.TextState.StrikeOut = true;

            // Optional: set font and color using Aspose.Pdf.Color (cross‑platform)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 20;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Position the fragment on the page (baseline at (100, 700))
            fragment.Position = new Position(100, 700);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document (PDF format) – using the standard Save method
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with strikethrough text saved to '{outputPath}'.");
    }
}