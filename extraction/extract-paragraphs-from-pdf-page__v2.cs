using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";
        const int targetPage = 22; // page numbers are 1‑based in Aspose.Pdf

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Use TextFragmentAbsorber (core API) to extract the textual content of the page.
            // ParagraphAbsorber is not available in the current Aspose.Pdf version, so the
            // equivalent core‑API class is TextFragmentAbsorber which returns TextFragment objects.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.Visit(doc.Pages[targetPage]);

            // Extract the text of each fragment (this corresponds to the paragraph markup).
            var fragments = absorber.TextFragments;
            string[] lines = new string[fragments.Count];
            for (int i = 0; i < fragments.Count; i++)
            {
                lines[i] = fragments[i].Text;
            }

            // Write the extracted paragraphs to the output text file
            File.WriteAllLines(outputPath, lines);
        }

        Console.WriteLine($"Paragraphs from page {targetPage} have been written to '{outputPath}'.");
    }
}
