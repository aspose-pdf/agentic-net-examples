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

        try
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Flatten all annotations (make them non‑editable visual elements)
                editor.FlatteningAnnotations();

                // Save the flattened PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}