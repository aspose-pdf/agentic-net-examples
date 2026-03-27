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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        long originalSize = new FileInfo(inputPath).Length;

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations();
            editor.Save(outputPath);
            editor.Close();
        }

        long newSize = new FileInfo(outputPath).Length;

        Console.WriteLine("Original file size: " + originalSize + " bytes");
        Console.WriteLine("Flattened file size: " + newSize + " bytes");
    }
}
