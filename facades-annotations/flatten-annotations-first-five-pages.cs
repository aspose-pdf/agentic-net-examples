using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_first5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor facade
        Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Retrieve all possible annotation types
            Aspose.Pdf.Annotations.AnnotationType[] allTypes =
                (Aspose.Pdf.Annotations.AnnotationType[])Enum.GetValues(typeof(Aspose.Pdf.Annotations.AnnotationType));

            // Flatten annotations on pages 1 through 5 (inclusive)
            editor.FlatteningAnnotations(1, 5, allTypes);

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"Annotations on the first five pages have been flattened. Output saved to '{outputPath}'.");
    }
}