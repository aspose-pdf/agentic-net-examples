using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldText    = "Hello";
        const string newText    = "Hi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file to the facade.
            editor.BindPdf(inputPath);

            // Replace all occurrences of oldText with newText.
            editor.ReplaceText(oldText, newText);

            // Save the modified PDF to the desired output file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}