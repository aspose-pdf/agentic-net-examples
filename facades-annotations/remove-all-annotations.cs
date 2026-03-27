using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotations();
            editor.Save(outputPath);
            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        finally
        {
            editor.Close();
        }
    }
}