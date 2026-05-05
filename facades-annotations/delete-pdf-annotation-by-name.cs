using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // The annotation name to delete; could be obtained from elsewhere
        string annotationName = "my-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete the annotation whose name is stored in the variable.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF into the editor.
            editor.BindPdf(inputPath);

            // Delete the annotation by its name.
            editor.DeleteAnnotation(annotationName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Deleted annotation '{annotationName}'. Saved to '{outputPath}'.");
    }
}
