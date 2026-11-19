using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "strikethrough.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages[1] is the first page – 1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is struck through.");

            // Enable strikethrough (strikeout) via TextState – note the correct property name is StrikeOut (capital O)
            fragment.TextState.StrikeOut = true;

            // Optionally set other visual properties (color, font size, etc.)
            fragment.TextState.FontSize = 14F;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document as PDF (Document.Save without options always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with strikethrough text saved to '{outputPath}'.");
    }
}
