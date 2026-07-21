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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Edit only the first page (page numbers are 1‑based)
                editor.ProcessPages = new int[] { 1 };

                // Set the transition type to Fade.
                // In Aspose.Pdf, a value of 0 corresponds to the Fade transition.
                editor.TransitionType = 0;

                // Set the transition duration to two seconds.
                editor.TransitionDuration = 2;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Saved PDF with slide effect to '{outputPath}'.");
    }
}