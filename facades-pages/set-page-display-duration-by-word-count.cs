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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Extract text of the current page using TextAbsorber (correct API)
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(doc.Pages[i]);
                string pageText = absorber.Text ?? string.Empty;

                // Simple word count (split on whitespace)
                int wordCount = pageText.Split(
                    new[] { ' ', '\t', '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries).Length;

                // Determine display duration (seconds) proportional to word count
                // Example: 1 second per 100 words, minimum 1 second
                int durationSeconds = Math.Max(1, wordCount / 100);

                // Set page transition duration using PdfPageEditor (correct API)
                // Transition type "Fade" corresponds to integer value 4 in Aspose.Pdf
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    editor.ProcessPages = new int[] { i }; // apply to current page only
                    editor.TransitionType = 4;               // Fade transition
                    editor.TransitionDuration = durationSeconds;
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (creation/saving rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
