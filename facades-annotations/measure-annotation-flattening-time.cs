using System;
using System.Diagnostics;
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

        // Initialize the annotation editor and bind the PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Measure the time taken to flatten all annotations
            Stopwatch stopwatch = Stopwatch.StartNew();
            editor.FlatteningAnnotations(); // flatten all annotations in the document
            stopwatch.Stop();

            Console.WriteLine($"Flattening time: {stopwatch.Elapsed}");

            // Save the flattened PDF
            editor.Save(outputPath);
        }
    }
}