using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class AnnotationMerger
{
    public static void MergeAnnotations(string basePdfPath, string[] annotationPdfPaths, string outputPdfPath)
    {
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        foreach (string path in annotationPdfPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Annotation source PDF not found: {path}");
                return;
            }
        }

        // Bind the base PDF to the editor and import annotations from the other PDFs
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(basePdfPath);
            editor.ImportAnnotations(annotationPdfPaths);
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations merged into '{outputPdfPath}'.");
    }

    // Example usage
    public static void Main()
    {
        string basePdf = "base.pdf";
        string[] sources = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string resultPdf = "merged_annotations.pdf";

        MergeAnnotations(basePdf, sources, resultPdf);
    }
}