using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase = "specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Extract raw text from the current page
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[pageNum].Accept(absorber);
                string pageText = absorber.Text;

                // Locate every occurrence of the target phrase (case‑insensitive)
                int searchStart = 0;
                int occurrence = 0;
                while ((searchStart = pageText.IndexOf(phrase, searchStart, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    occurrence++;
                    Console.WriteLine($"Page {pageNum}, occurrence {occurrence}, character offset {searchStart}");
                    searchStart += phrase.Length; // advance past the found phrase
                }
            }
        }
    }
}