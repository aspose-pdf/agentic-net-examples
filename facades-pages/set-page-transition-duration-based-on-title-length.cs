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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Extract visible text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages[i].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Determine transition duration based on title length (here using text length)
                    // Example: 1 second per 10 characters, capped at 10 seconds
                    int duration = Math.Min(pageText.Length / 10 + 1, 10);

                    // Configure the editor for the current page only
                    editor.ProcessPages = new int[] { i };
                    editor.TransitionDuration = duration;          // duration in seconds
                    editor.TransitionType = PdfPageEditor.DISSOLVE; // any transition style

                    // Apply the changes to the page
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }
}