using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the page range (inclusive). Adjust as needed.
        int startPage = 2; // first page in the range (1‑based)
        int endPage   = 5; // last page in the range (1‑based)

        // Ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Build an array of page numbers for the desired range.
                int[] pagesInRange = Enumerable.Range(startPage, endPage - startPage + 1).ToArray();

                // Specify which pages to edit.
                editor.ProcessPages = pagesInRange;

                // Set the transition duration to 1 second for each selected page.
                editor.TransitionDuration = 1; // seconds

                // Apply the changes to the document.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Transition duration set to 1 second for pages {startPage}-{endPage}.");
    }
}