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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Read all lines from the text file
        string[] lines = File.ReadAllLines(inputPath);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Obtain a monospace font (Courier)
            Font monoFont = FontRepository.FindFont("Courier");

            // Add each line as a separate paragraph with the monospace font
            foreach (string line in lines)
            {
                TextFragment fragment = new TextFragment(line);
                fragment.TextState.Font = monoFont;
                fragment.TextState.FontSize = 10; // optional size
                page.Paragraphs.Add(fragment);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}