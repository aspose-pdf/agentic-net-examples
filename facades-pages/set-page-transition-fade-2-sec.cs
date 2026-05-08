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

        // Use PdfPageEditor facade to edit page transition settings
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Apply changes only to the first page
            editor.ProcessPages = new int[] { 1 };

            // Set transition type to Fade (value 0) and duration to 2 seconds
            editor.TransitionType = 0;      // Fade
            editor.TransitionDuration = 2; // seconds

            // Apply the modifications
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}