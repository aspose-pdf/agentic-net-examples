using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Start timing
        Stopwatch timer = Stopwatch.StartNew();

        // Flatten all annotations using the correct Facades API method
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            // Correct method name – FlatteningAnnotations()
            editor.FlatteningAnnotations();
            editor.Save(outputPath);
        }

        // Stop timing
        timer.Stop();

        Console.WriteLine($"Annotations flattened in {timer.Elapsed.TotalSeconds:F2} seconds.");
    }
}
