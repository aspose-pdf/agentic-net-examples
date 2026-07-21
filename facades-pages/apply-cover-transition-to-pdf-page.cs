using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a sample input PDF with at least four pages so the example can run
        // in the sandbox where no external files exist.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add four blank pages (page numbers are 1‑based)
            for (int i = 0; i < 4; i++)
                seed.Pages.Add();

            // Save the seed file – this will be the file we later load and edit.
            seed.Save(inputPath);
        }

        // Load the PDF document we just created
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Edit only page 4 (1‑based indexing)
                editor.ProcessPages = new int[] { 4 };

                // Set the transition type to Cover using the integer value (4)
                editor.TransitionType = 4; // Cover transition

                // Set the transition duration to 1 second
                editor.TransitionDuration = 1;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page transition applied and saved to '{outputPath}'.");
    }
}
