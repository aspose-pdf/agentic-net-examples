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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Use PdfPageEditor to set page transition (BoxOut) for page 2
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Specify the page(s) to edit – pages are 1‑based
            editor.ProcessPages = new int[] { 2 };

            // TransitionType values are integer based in older Aspose.Pdf versions.
            // BoxOut corresponds to the integer value 4.
            editor.TransitionType = 4;          // BoxOut transition (zoom‑out effect)
            editor.TransitionDuration = 3;      // Duration in seconds

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
