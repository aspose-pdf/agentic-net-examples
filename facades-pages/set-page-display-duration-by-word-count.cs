using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_durations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages[pageNum].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words (split on whitespace)
                    int wordCount = pageText
                        .Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Length;

                    // Define reading speed: e.g., 200 words per second
                    const double wordsPerSecond = 200.0;
                    int durationSeconds = (int)Math.Ceiling(wordCount / wordsPerSecond);

                    // Ensure a minimum duration of 1 second for pages with text
                    if (durationSeconds < 1 && wordCount > 0)
                        durationSeconds = 1;

                    // Apply the duration to the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.DisplayDuration = durationSeconds;
                    editor.ApplyChanges();
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with page display durations: {outputPath}");
    }
}