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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Specify that only page 5 should be edited (1‑based indexing)
                editor.ProcessPages = new int[] { 5 };

                // Center the original content on the result page
                editor.HorizontalAlignment = HorizontalAlignment.Center;

                // Set the display duration for the edited page to 4 seconds
                editor.DisplayDuration = 4;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page alignment and duration updated. Saved to '{outputPath}'.");
    }
}