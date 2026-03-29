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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Example operation: remove any open document action
            editor.RemoveDocumentOpenAction();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}