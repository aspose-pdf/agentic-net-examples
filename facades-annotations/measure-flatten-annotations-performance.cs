using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input500.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Stopwatch sw = Stopwatch.StartNew();

        // Load PDF, flatten all annotations, and save
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // load
            editor.FlatteningAnnotations();          // flatten all annotations
            editor.Save(outputPath);                  // save
        }

        sw.Stop();
        Console.WriteLine($"Flattening completed in {sw.Elapsed.TotalSeconds:F2} seconds.");
    }
}