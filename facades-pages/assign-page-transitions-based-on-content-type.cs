using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect its pages
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Simple content‑type check: does the page contain images?
                    bool hasImages = doc.Pages[pageNum].Resources.Images.Count > 0;

                    // Specify which page to edit
                    editor.ProcessPages = new int[] { pageNum };

                    // Assign a distinct transition based on the content type
                    if (hasImages)
                    {
                        // Use vertical blinds for pages with images
                        editor.TransitionType = PdfPageEditor.BLINDV;
                    }
                    else
                    {
                        // Use dissolve effect for text‑only pages
                        editor.TransitionType = PdfPageEditor.DISSOLVE;
                    }

                    // Set a common transition duration (seconds)
                    editor.TransitionDuration = 2;

                    // Apply the transition settings to the current page
                    editor.ApplyChanges();
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}