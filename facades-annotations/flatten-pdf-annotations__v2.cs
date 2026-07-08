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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor facade on the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Flatten all annotations – this converts them to static graphics
                // while preserving their visual appearance.
                editor.FlatteningAnnotations();

                // Save the resulting PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}