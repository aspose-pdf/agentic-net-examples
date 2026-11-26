using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.txt";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load all lines from the pre‑formatted text file
        string[] lines = File.ReadAllLines(inputPath);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Retrieve a monospace font (Courier) from the built‑in font repository
            Font monoFont = FontRepository.FindFont("Courier");

            // Add each line as a separate paragraph
            foreach (string line in lines)
            {
                // Create a text fragment for the line
                TextFragment tf = new TextFragment(line);
                // Apply the monospace font
                tf.TextState.Font = monoFont;
                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(tf);
            }

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}