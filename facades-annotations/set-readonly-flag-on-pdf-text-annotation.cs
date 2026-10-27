using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to access its annotations
        Document pdfDoc = new Document(inputPath);
        if (pdfDoc.Pages.Count == 0)
        {
            Console.WriteLine("Document contains no pages.");
            File.Copy(inputPath, outputPath, true);
            return;
        }

        // For this example we work with the first page; adjust as needed
        Page page = pdfDoc.Pages[1];
        Annotation[] pageAnnotations = page.Annotations.ToArray();

        if (pageAnnotations == null || pageAnnotations.Length == 0)
        {
            Console.WriteLine("No annotations found in the document.");
            File.Copy(inputPath, outputPath, true);
            return;
        }

        // Select the annotation you want to modify (first one here)
        Annotation targetAnnotation = pageAnnotations[0];

        // Apply the ReadOnly flag
        targetAnnotation.Flags = AnnotationFlags.ReadOnly;

        // Write the modified annotation back using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            // PdfAnnotationEditor.ModifyAnnotations expects (pageNumber, annotationIndex, annotation)
            // page numbers are 1‑based, annotation indexes are 0‑based
            editor.ModifyAnnotations(1, 0, targetAnnotation);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Read‑only flag applied and saved to '{outputPath}'.");
    }
}
