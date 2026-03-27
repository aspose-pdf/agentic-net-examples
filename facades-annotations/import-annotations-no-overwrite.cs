using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for AnnotationType enum

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string outputPath = "merged_annotations.pdf";
        string[] sourcePaths = new string[] { "source1.pdf", "source2.pdf" };

        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        using (Document targetDoc = new Document(targetPath))
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(targetDoc))
            {
                // Import annotations from the source PDFs without overwriting existing ones.
                // The current Aspose.Pdf API overload expects an array of AnnotationType.
                // Passing null imports all annotation types and, by design, does not overwrite
                // annotations that already exist in the target document.
                editor.ImportAnnotations(sourcePaths, null);

                // Save the document with merged annotations
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPath}'.");
    }
}
