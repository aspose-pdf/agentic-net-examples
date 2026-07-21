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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document.
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Define the range of pages to edit (e.g., pages 2 through 5).
            // Page numbers are 1‑based.
            editor.ProcessPages = new int[] { 2, 3, 4, 5 };

            // Set the transition duration to one second for the selected pages.
            editor.TransitionDuration = 1; // seconds

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified document.
            doc.Save(outputPath);

            // Release resources held by the facade.
            editor.Close();
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}