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

        // PdfContentEditor implements IDisposable, so using ensures proper disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "Hello" with "Hi"
            editor.ReplaceText("Hello", "Hi");

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}