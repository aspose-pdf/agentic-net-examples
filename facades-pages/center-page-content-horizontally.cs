using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_centered.pdf";
        const string text = "Centered Text on Page 2";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level API (Document)
        Document doc = new Document(inputPath);

        // Create a text fragment and set its horizontal alignment to Center
        TextFragment fragment = new TextFragment(text);
        fragment.TextState.FontSize = 12;                     // optional size
        fragment.TextState.Font = FontRepository.FindFont("Arial"); // optional font
        fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

        // Add the fragment to page 2 (Aspose.Pdf uses 1‑based page indexing)
        doc.Pages[2].Paragraphs.Add(fragment);

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Centered content saved to '{outputPath}'.");
    }
}
