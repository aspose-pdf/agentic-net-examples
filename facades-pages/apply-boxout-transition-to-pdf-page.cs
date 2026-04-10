using System;
using System.IO;
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

        // Initialize the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Edit only page 2
            editor.ProcessPages = new int[] { 2 };

            // Set transition to BoxOut (outward box) and duration to 3 seconds
            editor.TransitionType = PdfPageEditor.OUTBOX;
            editor.TransitionDuration = 3;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}