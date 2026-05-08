using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the annotation editor, bind the PDF, flatten all annotations, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF document.
            editor.FlatteningAnnotations();          // Flatten all annotations to non‑editable visual elements.
            editor.Save(outputPath);                  // Save the flattened PDF.
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}