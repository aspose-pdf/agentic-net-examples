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

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            // Set rotation of page 4 to 180 degrees
            editor.PageRotations[4] = 180;
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated to 180 degrees and saved to '{outputPath}'.");
    }
}