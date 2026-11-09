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

        // PdfContentEditor does not implement IDisposable, so no using block is needed.
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF file.
        editor.BindPdf(inputPath);

        // Replace every occurrence of "Draft" with "Final".
        editor.ReplaceText("Draft", "Final");

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"All occurrences of \"Draft\" replaced with \"Final\". Saved to '{outputPath}'.");
    }
}