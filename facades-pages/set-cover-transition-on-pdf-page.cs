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
        Document doc = new Document(inputPath);
        // Use PdfPageEditor to set a transition on a specific page
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Process only page 4 (pages are 1‑based)
            editor.ProcessPages = new int[] { 4 };

            // Set transition type to Cover (integer value 4) and duration to 1 second
            editor.TransitionType = 4; // Cover transition
            editor.TransitionDuration = 1;

            // Apply the changes and save the document
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Cover transition on page 4: {outputPath}");
    }
}
