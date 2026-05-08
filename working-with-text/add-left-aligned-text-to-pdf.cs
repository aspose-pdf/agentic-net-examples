using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, TextState, HorizontalAlignment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Aligned left margin text.");

            // Set the horizontal alignment to Left via TextState
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Add the fragment to the first page (or any target page)
            Page page = doc.Pages[1];               // 1‑based indexing
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with left‑aligned text to '{outputPath}'.");
    }
}