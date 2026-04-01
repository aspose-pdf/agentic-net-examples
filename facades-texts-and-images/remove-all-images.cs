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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteImage();
        editor.Save(outputPath);
        Console.WriteLine("All images removed. Saved to '" + outputPath + "'.");
    }
}