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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Restrict editing to page 6 (1‑based indexing)
            editor.ProcessPages = new int[] { 6 };

            // Set transition duration to zero seconds to disable animation
            editor.TransitionDuration = 0;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set to zero on page 6. Saved to '{outputPath}'.");
    }
}