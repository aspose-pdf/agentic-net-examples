using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path with the transition effect applied
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document and edit page 3 using PdfPageEditor
        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Target only page 3 (pages are 1‑based)
            editor.ProcessPages = new int[] { 3 };

            // Set a split transition (horizontal split-in effect)
            editor.TransitionType = PdfPageEditor.SPLITHIN;

            // Set the transition duration to 2 seconds
            editor.TransitionDuration = 2;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}