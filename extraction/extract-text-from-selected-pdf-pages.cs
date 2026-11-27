using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages we want to extract (1‑based indexing)
            int[] pagesToExtract = { 2, 4 };
            var combinedText = new StringBuilder();

            foreach (int pageNumber in pagesToExtract)
            {
                // Guard against out‑of‑range page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                // TextAbsorber extracts the raw text of a page.
                // It works even when ParagraphAbsorber is not available in the referenced version.
                var absorber = new TextAbsorber();
                doc.Pages[pageNumber].Accept(absorber);
                combinedText.AppendLine(absorber.Text);
            }

            // Save the combined text to a plain‑text file
            File.WriteAllText(outputPath, combinedText.ToString());
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}
