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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Flatten all annotations (convert them to non‑editable visual elements)
            editor.FlatteningAnnotations();

            // Save the flattened PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}