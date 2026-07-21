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
            // Create a PdfPageEditor facade bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Retrieve the current page
                    Page page = doc.Pages[pageNum];

                    // Determine a simple content type:
                    // If the page contains at least one image, treat it as "image" content,
                    // otherwise treat it as "text" content.
                    bool hasImages = page.Resources.Images.Count > 0;

                    // Configure the editor to work on the current page only
                    editor.ProcessPages = new int[] { pageNum };

                    // Assign a distinct transition type based on the content type
                    if (hasImages)
                    {
                        // Use a vertical blinds transition for image pages
                        editor.TransitionType = PdfPageEditor.BLINDV;
                    }
                    else
                    {
                        // Use a dissolve transition for text‑only pages
                        editor.TransitionType = PdfPageEditor.DISSOLVE;
                    }

                    // Optional: set the duration of the transition (in seconds)
                    // TransitionDuration expects an integer value (seconds), not a float.
                    editor.TransitionDuration = 2; // 2 seconds

                    // Apply the changes for the current page
                    editor.ApplyChanges();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}
