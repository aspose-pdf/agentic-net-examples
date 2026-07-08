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

        // Load the PDF document inside a using block (document disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Iterate through all pages (1‑based indexing rule)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages[pageNum].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Simple word count (split on whitespace)
                    int wordCount = pageText.Split(new char[] { ' ', '\t', '\r', '\n' },
                                                   StringSplitOptions.RemoveEmptyEntries).Length;

                    // Estimate display duration:
                    // Assume an average reading speed of 200 words per minute (≈3.33 words per second)
                    // Duration is rounded up to the next whole second.
                    double seconds = Math.Ceiling(wordCount / 3.33);
                    int duration = (int)seconds;

                    // Configure the editor to affect only the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.DisplayDuration = duration; // set duration for this page
                    editor.ApplyChanges(); // apply the change
                }
            }

            // Save the modified document (save rule – explicit call inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with per‑page display durations: {outputPath}");
    }
}