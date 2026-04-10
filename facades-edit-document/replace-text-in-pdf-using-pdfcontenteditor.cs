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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable; the using block guarantees disposal.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF to be edited.
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "OldText" with "NewText".
            editor.ReplaceText("OldText", "NewText");

            // Persist the changes to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}