using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document for text extraction
        using (Document doc = new Document(inputPath))
        // Create a PdfPageEditor facade to modify page display durations
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the same PDF to the editor
            editor.BindPdf(doc);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                // Correct overload: pass the Page object, not (Document, pageNumber)
                absorber.Visit(doc.Pages[pageNum]);
                string pageText = absorber.Text ?? string.Empty;

                // Count words (simple split on whitespace)
                int wordCount = pageText.Split(
                    new[] { ' ', '\n', '\r', '\t' },
                    StringSplitOptions.RemoveEmptyEntries).Length;

                // Determine display duration (e.g., 1 second per 100 words, minimum 1 second)
                int durationSeconds = Math.Max(1, wordCount / 100);

                // Configure the editor to affect only the current page
                editor.ProcessPages = new[] { pageNum };
                editor.DisplayDuration = durationSeconds;

                // Apply the change to the page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with per‑page display durations: {outputPath}");
    }
}
