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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply the transition to all pages (1‑based indexing)
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();

                // Integer value for the Fade transition (as defined by Aspose.Pdf)
                // Fade is typically represented by the value 10 in the internal enum.
                editor.TransitionType = 10;      // Fade transition
                editor.TransitionDuration = 2;   // 2‑second duration

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}