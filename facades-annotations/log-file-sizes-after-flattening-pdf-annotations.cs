using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Log original file size
        long originalSize = new FileInfo(inputPath).Length;
        Console.WriteLine($"Original file size: {originalSize} bytes");

        // Flatten annotations using PdfAnnotationEditor (Facades API)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.FlatteningAnnotations(); // flatten all annotations
        editor.Save(outputPath);        // save flattened PDF

        // Log new file size
        long newSize = new FileInfo(outputPath).Length;
        Console.WriteLine($"Flattened file size: {newSize} bytes");
    }
}