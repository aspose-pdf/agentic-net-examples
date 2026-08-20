using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Simple content‑type detection:
                // If the page contains any images, treat it as an "image" page,
                // otherwise treat it as a "text" page.
                bool hasImages = page.Resources.Images.Count > 0;

                // Configure the editor to edit only the current page
                editor.ProcessPages = new int[] { pageNum };

                // Assign a distinct transition type based on the content
                if (hasImages)
                {
                    // Vertical blinds for image pages
                    editor.TransitionType = PdfPageEditor.BLINDV;
                }
                else
                {
                    // Dissolve effect for text pages
                    editor.TransitionType = PdfPageEditor.DISSOLVE;
                }

                // Optional: set transition duration (in seconds)
                editor.TransitionDuration = 2;

                // Apply the changes to the current page
                editor.ApplyChanges();
            }

            // Save the modified PDF using the facade's Save method
            editor.Save(outputPath);

            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"PDF saved with page transitions: {outputPath}");
    }
}