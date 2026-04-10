using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf.Annotations;      // AnnotationType enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_first5.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Retrieve all possible annotation types
        AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

        // Flatten annotations on pages 1 through 5 (inclusive)
        editor.FlatteningAnnotations(1, 5, allTypes);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close(); // Release resources

        Console.WriteLine($"Annotations on the first 5 pages have been flattened and saved to '{outputPath}'.");
    }
}