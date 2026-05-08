using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input_500_pages.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Start timing
        Stopwatch sw = Stopwatch.StartNew();

        // Use PdfAnnotationEditor facade to flatten annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);                 // Load the PDF
            editor.FlatteningAnnotations();            // Flatten all annotations
            editor.Save(outputPath);                   // Save the result
        }

        // Stop timing
        sw.Stop();
        Console.WriteLine($"Flattening completed in {sw.Elapsed.TotalSeconds:F2} seconds.");
    }
}