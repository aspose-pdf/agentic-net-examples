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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Flatten all annotations in the document
            editor.FlatteningAnnotations();

            // Save the flattened PDF
            editor.Save(outputPath);

            // Close the editor (optional, Dispose will be called by GC)
            editor.Close();
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}