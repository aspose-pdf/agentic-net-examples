using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    // Adjust this factor to control how many seconds per word (e.g., 0.01 sec per word)
    const double SecondsPerWord = 0.01;

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document to read text per page
        using (Document doc = new Document(inputPdf))
        {
            // Prepare the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the same document instance to the editor
                editor.BindPdf(doc);

                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                    doc.Pages[pageNum].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words (split on whitespace)
                    int wordCount = pageText.Split(
                        new char[] { ' ', '\n', '\r', '\t' },
                        StringSplitOptions.RemoveEmptyEntries).Length;

                    // Compute duration in seconds (rounded to nearest whole number)
                    int duration = (int)Math.Round(wordCount * SecondsPerWord);
                    if (duration < 1) duration = 1; // Minimum of 1 second per page

                    // Apply the duration to the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.DisplayDuration = duration;
                    editor.ApplyChanges();
                }

                // Save the modified PDF with per‑page display durations
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}