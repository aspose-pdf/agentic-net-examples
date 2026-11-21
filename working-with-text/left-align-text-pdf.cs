using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string text = "Left aligned text added to the page.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired content
            TextFragment tf = new TextFragment(text);

            // Align the text to the left margin
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Add the fragment to the first page (pages are 1‑based)
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}