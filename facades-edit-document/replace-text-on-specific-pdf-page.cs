using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to be replaced on page 3
        const string oldText = "TextToReplace";
        const string newText = "NewReplacement";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a Facades API that works directly on PDF files.
        // It does NOT implement IDisposable, so we do not wrap it in a using block.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Replace the specified text only on page 3 (pages are 1‑based)
        editor.ReplaceText(oldText, newText, 3);

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Text on page 3 replaced and saved to '{outputPath}'.");
    }
}