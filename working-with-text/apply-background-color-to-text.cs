using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "colored_text.pdf";

        // Ensure the output directory exists (handle possible null from GetDirectoryName)
        string outDir = Path.GetDirectoryName(outputPath) ?? string.Empty;
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Highlighted text with background color");

            // Set the background color using Aspose.Pdf.Color (cross‑platform)
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Optional: set additional text properties
            fragment.TextState.FontSize = 14;

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the PDF; Document.Save writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
