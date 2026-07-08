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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Determine content type: image‑heavy vs text‑only
                    bool hasImages = page.Resources.Images.Count > 0;

                    // Restrict editing to the current page
                    editor.ProcessPages = new int[] { i };

                    // Assign distinct transition types based on content
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

                    // Set a uniform transition duration (in seconds)
                    editor.TransitionDuration = 2;

                    // Apply the changes to the current page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (save without explicit SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page transitions to '{outputPath}'.");
    }
}