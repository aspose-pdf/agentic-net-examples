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
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Aspose.Pdf.Page does not expose a Title property.
                    // For demonstration we synthesize a title based on the page number.
                    // Replace this logic with the actual source of titles if available.
                    string title = $"Page {i}"; // placeholder title

                    // Determine transition duration based on title length.
                    // Example: 1 second per 10 characters, minimum 1 second.
                    int duration = Math.Max(1, title.Length / 10);

                    // Configure the editor to affect only the current page
                    editor.ProcessPages = new int[] { i };

                    // Set the transition duration (in seconds) and a transition type.
                    editor.TransitionDuration = duration;
                    editor.TransitionType = PdfPageEditor.BLINDH; // Example transition style

                    // Apply the changes to the page
                    editor.ApplyChanges();
                }
            }

            // Save the modified document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}
