using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "large_input.pdf";   // 500‑page PDF
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Measure the flattening operation
        Stopwatch sw = new Stopwatch();

        // Load and process the PDF using the facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the document

            sw.Start();                         // Start timing
            editor.FlatteningAnnotations();    // Flatten all annotations
            sw.Stop();                          // Stop timing

            editor.Save(outputPath);            // Save the modified PDF
        }

        Console.WriteLine($"Flattening annotations completed in {sw.Elapsed.TotalSeconds:F2} seconds.");
    }
}