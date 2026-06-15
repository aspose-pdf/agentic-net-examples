using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfAnnotationEditor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(inputPath);

        // Flatten all annotations: their visual appearance is preserved,
        // but interactive properties (links, comments, etc.) are removed.
        editor.FlatteningAnnotations();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}