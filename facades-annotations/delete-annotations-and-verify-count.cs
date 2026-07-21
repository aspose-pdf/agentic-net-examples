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
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Count annotations in the original document
        int beforeCount = 0;
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                beforeCount += doc.Pages[i].Annotations.Count;
            }
        }

        // Delete all annotations using PdfAnnotationEditor and save the result
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotations(); // removes all annotations
            editor.Save(outputPath);    // save the modified PDF
        }

        // Count annotations after deletion
        int afterCount = 0;
        using (Document cleaned = new Document(outputPath))
        {
            for (int i = 1; i <= cleaned.Pages.Count; i++) // 1‑based indexing
            {
                afterCount += cleaned.Pages[i].Annotations.Count;
            }
        }

        Console.WriteLine($"Annotations before deletion: {beforeCount}");
        Console.WriteLine($"Annotations after deletion: {afterCount}");
    }
}