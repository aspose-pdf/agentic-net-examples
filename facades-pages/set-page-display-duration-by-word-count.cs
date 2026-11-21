using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

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

        // Load the PDF to extract text and compute per‑page durations
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count; // correct way to get page count

            // Prepare a PdfPageEditor that will be used to set the display duration per page
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                for (int i = 1; i <= pageCount; i++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                    doc.Pages[i].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Simple word count
                    int wordCount = 0;
                    if (!string.IsNullOrWhiteSpace(pageText))
                    {
                        wordCount = pageText.Split(new char[] { ' ', '\t', '\r', '\n' },
                                                   StringSplitOptions.RemoveEmptyEntries).Length;
                    }

                    // Set display duration proportionally (e.g., 0.5 s per word, minimum 1 s)
                    double durationSeconds = Math.Max(1.0, wordCount * 0.5);
                    int durationInt = (int)Math.Ceiling(durationSeconds); // PdfPageEditor expects an int (seconds)

                    // Apply the duration to the current page using PdfPageEditor
                    editor.ProcessPages = new int[] { i };
                    editor.DisplayDuration = durationInt;
                    editor.ApplyChanges();
                }

                // Save the modified PDF with the new per‑page timings
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
