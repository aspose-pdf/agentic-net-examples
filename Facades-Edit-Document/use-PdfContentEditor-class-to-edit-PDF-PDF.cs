using System;
using System.IO;
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

        // Create the PdfContentEditor, bind the source PDF, perform an edit, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file for editing.
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "Sample" with "Example".
            editor.ReplaceText("Sample", "Example");

            // Persist the changes to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}