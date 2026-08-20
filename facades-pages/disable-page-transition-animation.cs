using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Edit only page 6 (1‑based indexing)
                editor.ProcessPages = new int[] { 6 };
                // Disable transition animation by setting duration to zero seconds
                editor.TransitionDuration = 0;
                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}