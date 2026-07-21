using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input500.pdf";
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
            // Load the PDF
            editor.BindPdf(inputPath);

            // Flatten all annotations in the document
            editor.FlatteningAnnotations();

            // Save the result
            editor.Save(outputPath);
        }

        // Stop timing
        sw.Stop();
        Console.WriteLine($"Flattening annotations completed in {sw.Elapsed.TotalSeconds:F2} seconds.");
    }
}