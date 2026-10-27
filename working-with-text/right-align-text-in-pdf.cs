using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "right_aligned.pdf";
        const string text = "Right aligned text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Create a TextFragment containing the desired text
            TextFragment tf = new TextFragment(text);

            // Right‑align the text by setting HorizontalAlignment on the TextState
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Position the fragment (Y coordinate only matters for right alignment)
            tf.Position = new Position(0, 800); // X is ignored when aligned right

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved right‑aligned PDF to '{outputPath}'.");
    }
}