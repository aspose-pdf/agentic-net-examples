using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Edit only the current page
                    editor.ProcessPages = new int[] { i };
                    // Set display duration (seconds) equal to the page index
                    editor.DisplayDuration = i;
                    // Apply the change to the page
                    editor.ApplyChanges();
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with per‑page display durations to '{outputPath}'.");
    }
}