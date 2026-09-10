using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Log original file size
        long originalSize = new FileInfo(inputPath).Length;

        // Flatten all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(); // flatten all annotations
            editor.Save(outputPath);
        }

        // Log new file size after flattening
        long newSize = new FileInfo(outputPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"New size after flattening: {newSize} bytes");
    }
}