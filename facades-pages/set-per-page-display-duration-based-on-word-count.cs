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

        // Average reading speed (words per second). Adjust as needed.
        const double wordsPerSecond = 200.0;

        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the document to the facade.
            editor.BindPdf(doc);

            int pageCount = doc.Pages.Count;

            for (int i = 1; i <= pageCount; i++)
            {
                // Extract text from the current page.
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Count words (splitting on whitespace).
                int wordCount = pageText.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;

                // Compute display duration in seconds (rounded up).
                int durationSeconds = (int)Math.Ceiling(wordCount / wordsPerSecond);
                if (durationSeconds < 1) durationSeconds = 1; // Minimum of 1 second.

                // Apply the duration to the current page using PdfPageEditor.
                editor.ProcessPages = new int[] { i };
                editor.DisplayDuration = durationSeconds;
                editor.ApplyChanges();
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with per‑page display durations: {outputPath}");
    }
}