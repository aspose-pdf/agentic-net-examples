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
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is struck through");

            // Enable strikethrough (strikeout) via the TextState – note the correct property name is **StrikeOut**
            fragment.TextState.StrikeOut = true;

            // Optionally set font size and color for better visibility
            fragment.TextState.FontSize = 14F;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF; Document.Save(string) writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with strikethrough text saved to '{outputPath}'.");
    }
}
