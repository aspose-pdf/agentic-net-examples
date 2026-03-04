using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Create a PdfContentEditor instance, bind the PDF, perform editing, and save.
        // PdfContentEditor implements IDisposable, so we use a using block for deterministic cleanup.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPath);

            // Example operation: replace all occurrences of "Hello" with "Hi"
            // ReplaceText(string oldValue, string newValue) edits the entire document.
            editor.ReplaceText("Hello", "Hi");

            // Save the modified PDF to the specified output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF editing completed. Output saved to '{outputPath}'.");
    }
}