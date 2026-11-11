using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor that will be reused for each page
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // NOTE: PageInfo does not expose a Title property in the current API.
                    // As a fallback, we use the length of the page number string as a proxy
                    // for title length. Replace this logic with the correct title extraction
                    // method if your PDF stores titles elsewhere.
                    int titleLength = page.Number.ToString().Length;

                    // Determine transition duration (seconds). Longer titles → longer duration.
                    // Base duration is 1 second; each character adds 0.2 seconds.
                    double duration = 1.0 + (titleLength * 0.2);
                    int durationSec = (int)Math.Ceiling(duration); // PdfPageEditor expects an integer value

                    // Configure the editor for the current page
                    editor.ProcessPages = new int[] { i };
                    // Example transition type: 4 corresponds to "Cover" (see Aspose.Pdf documentation)
                    editor.TransitionType = 4;
                    editor.TransitionDuration = durationSec;
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}
