using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Extract text fragments from the current page
                var absorber = new TextFragmentAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                pdfDoc.Pages[pageNum].Accept(absorber);

                // Sort fragments by Y coordinate (top‑to‑bottom). Higher Y = higher on page.
                var orderedFragments = absorber.TextFragments
                                               .Cast<TextFragment>()
                                               .OrderByDescending(f => f.Rectangle.LLY);

                // Combine the sorted fragment texts into a single string
                string pageText = string.Join(Environment.NewLine,
                                              orderedFragments.Select(f => f.Text));

                // Write the ordered text to a per‑page .txt file
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.txt");
                File.WriteAllText(outputPath, pageText);
            }
        }

        Console.WriteLine("Text extraction and ordering completed.");
    }
}
