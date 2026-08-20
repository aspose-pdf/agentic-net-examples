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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so a using block ensures it is closed automatically.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "Hello" with "Hi".
            editor.ReplaceText("Hello", "Hi");

            // Persist the changes to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}