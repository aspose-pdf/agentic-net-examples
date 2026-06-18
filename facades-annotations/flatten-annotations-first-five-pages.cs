using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor and bind the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Retrieve all possible annotation types
                AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                // Flatten annotations on pages 1 through 5 (inclusive)
                editor.FlatteningAnnotations(1, 5, allTypes);

                // Save the resulting PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations on the first five pages have been flattened. Output saved to '{outputPath}'.");
    }
}