using System;
using System.IO;
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
            // Factor: seconds per word (adjust as needed)
            const double secondsPerWord = 0.05; // e.g., 0.05 sec ≈ 20 words per second

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[pageNum].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Count words
                int wordCount = 0;
                if (!string.IsNullOrWhiteSpace(pageText))
                {
                    char[] delimiters = { ' ', '\t', '\r', '\n' };
                    wordCount = pageText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                }

                // Compute display duration (minimum 1 second)
                int duration = Math.Max(1, (int)Math.Round(wordCount * secondsPerWord));

                // Apply the duration to the current page using PdfPageEditor (Facade API)
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);                     // Bind the document
                    editor.ProcessPages = new int[] { pageNum }; // Target only this page
                    editor.DisplayDuration = duration;       // Set duration in seconds
                    editor.ApplyChanges();                   // Commit the change
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved with per‑page display durations: {outputPath}");
        }
    }
}