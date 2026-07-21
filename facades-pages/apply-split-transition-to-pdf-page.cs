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
            // Initialize PdfPageEditor and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Specify that only page 3 will be edited (pages are 1‑based)
                editor.ProcessPages = new int[] { 3 };

                // Set the transition effect to a split (horizontal in‑split used here)
                editor.TransitionType = PdfPageEditor.SPLITHIN; // IN Horizontal Split

                // Set the transition duration to 2 seconds
                editor.TransitionDuration = 2;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}