using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

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

        // Initialize the annotation editor and bind the PDF file
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Retrieve all possible annotation types
        AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

        // Flatten annotations on pages 1 through 5 (inclusive)
        editor.FlatteningAnnotations(1, 5, allTypes);

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotations on first five pages flattened and saved to '{outputPath}'.");
    }
}
