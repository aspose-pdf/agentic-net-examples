using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Apply transition and display settings page by page
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    // Edit only the current page
                    editor.ProcessPages = new int[] { i };

                    // Choose a transition type based on page index (example pattern)
                    switch (i % 4)
                    {
                        case 0:
                            editor.TransitionType = PdfPageEditor.BLINDH; // vertical blinds
                            break;
                        case 1:
                            editor.TransitionType = PdfPageEditor.DISSOLVE; // dissolve effect
                            break;
                        case 2:
                            editor.TransitionType = PdfPageEditor.TBWIPE; // top‑bottom wipe
                            break;
                        case 3:
                            editor.TransitionType = PdfPageEditor.LRWIPE; // left‑right wipe
                            break;
                    }

                    // Duration of the transition effect (seconds)
                    editor.TransitionDuration = 1;

                    // How long the page stays visible before moving to the next (seconds)
                    editor.DisplayDuration = 4;

                    // Apply the changes to the current page
                    editor.ApplyChanges();
                }

                // Save the modified PDF as a slideshow
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}