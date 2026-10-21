using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // needed for AnnotationType

class Program
{
    static void Main()
    {
        const string sourcePdf = "newAnnotations.pdf";   // PDF containing annotations to import
        const string destinationPdf = "existing.pdf";   // PDF that already has annotations
        const string outputPdf = "mergedAnnotations.pdf";

        // Verify that both PDFs exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        // Copy the destination PDF to a new file so the original remains unchanged
        File.Copy(destinationPdf, outputPdf, true);

        // Create the annotation editor and bind it to the copied PDF
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(outputPdf);

        // Import annotations from the source PDF while preserving existing ones.
        // Pass a string array with the source file and a null AnnotationType array.
        annotationEditor.ImportAnnotations(new string[] { sourcePdf }, null);

        // Save the changes back to the output file
        annotationEditor.Save(outputPdf);

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPdf}'.");
    }
}
