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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Measure the flattening operation
            Stopwatch sw = Stopwatch.StartNew();

            // PdfAnnotationEditor implements IDisposable, use a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF
                editor.BindPdf(inputPath);

                // Flatten all annotations (correct method name)
                editor.FlatteningAnnotations();

                // Save the modified PDF
                editor.Save(outputPath);
            }

            sw.Stop();

            Console.WriteLine($"Flattening annotations completed in {sw.Elapsed.TotalSeconds:F2} seconds.");
            Console.WriteLine($"Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
