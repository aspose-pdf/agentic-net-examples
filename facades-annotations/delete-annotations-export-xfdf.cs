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
        const string outputPath = "output_without_text_annots.pdf";
        const string xfdfPath = "remaining_annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and delete annotations of a specific type (e.g., TextAnnotation)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Delete annotations of type TextAnnotation.
                // Iterate backwards because Delete shifts indices.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation annot = page.Annotations[i];
                    if (annot is TextAnnotation)
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            // Save the PDF after deletion
            doc.Save(outputPath);
        }

        // Export the remaining annotations to XFDF using the correct overload
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            annotEditor.BindPdf(outputPath);
            using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // ExportAnnotationsXfdf(stream, startPage, endPage, annotationTypeNames)
                // Pass (string[])null to export all annotation types.
                annotEditor.ExportAnnotationsXfdf(fs, 1, annotEditor.Document.Pages.Count, (string[])null);
            }
        }

        Console.WriteLine($"Remaining annotations exported to '{xfdfPath}'.");
    }
}
