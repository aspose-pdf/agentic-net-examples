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
            editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;
            editor.Save(outputPath);
        }

        Console.WriteLine($"Horizontal alignment applied and saved to '{outputPath}'.");
    }
}