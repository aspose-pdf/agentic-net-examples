using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);
        int rotation = editor.GetPageRotation(4);
        Console.WriteLine($"Rotation of page 4: {rotation} degrees");
    }
}
