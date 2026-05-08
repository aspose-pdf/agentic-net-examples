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

        // Count annotations before deletion
        int beforeCount = 0;
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                AnnotationCollection annColl = doc.Pages[i].Annotations;
                beforeCount += annColl.Count;
            }
        }

        Console.WriteLine($"Annotations before deletion: {beforeCount}");

        // Delete all annotations using PdfAnnotationEditor and save the result
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotations();               // removes every annotation
            editor.Save(outputPath);                  // persist changes
        }

        // Count annotations after deletion
        int afterCount = 0;
        using (Document doc = new Document(outputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                afterCount += doc.Pages[i].Annotations.Count;
            }
        }

        Console.WriteLine($"Annotations after deletion: {afterCount}");
        Console.WriteLine(afterCount == 0
            ? "All annotations removed successfully."
            : "Some annotations remain.");
    }
}