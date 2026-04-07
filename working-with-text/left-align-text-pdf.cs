using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "aligned_text.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment text = new TextFragment("This text is left‑aligned on the page.");

            // Set the horizontal alignment of the text to Left
            // HorizontalAlignment.Left aligns text to the left margin;
            // HorizontalAlignment.None is equivalent to Left.
            text.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(text);

            // Save the document; no SaveOptions needed for PDF output
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}