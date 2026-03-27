using System;
using System.IO;
using System.Diagnostics;
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

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Flatten all annotations in the document
        editor.FlatteningAnnotations();

        stopwatch.Stop();

        editor.Save(outputPath);

        Console.WriteLine($"Flattening completed in {stopwatch.Elapsed.TotalSeconds} seconds.");
    }
}
