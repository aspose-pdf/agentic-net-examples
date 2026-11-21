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
        const string outputPath = "output_deleted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Count annotations before deletion ----------
        int beforeCount = 0;
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate safely
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                beforeCount += page.Annotations.Count;
            }
        }
        Console.WriteLine($"Annotations before deletion: {beforeCount}");

        // ---------- Delete all annotations using PdfAnnotationEditor ----------
        // PdfAnnotationEditor does NOT implement IDisposable; do NOT wrap in using
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);               // load the PDF
        editor.DeleteAnnotations();               // remove every annotation
        editor.Save(outputPath);                  // write the result

        // ---------- Count annotations after deletion ----------
        int afterCount = 0;
        using (Document doc = new Document(outputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                afterCount += page.Annotations.Count;
            }
        }
        Console.WriteLine($"Annotations after deletion: {afterCount}");

        // ---------- Verify that the expected number of items were removed ----------
        int removed = beforeCount - afterCount;
        Console.WriteLine($"Annotations removed: {removed}");
        if (afterCount == 0 && removed == beforeCount)
        {
            Console.WriteLine("DeleteAnnotations succeeded: all annotations were removed.");
        }
        else
        {
            Console.WriteLine("DeleteAnnotations may have failed or only partially removed annotations.");
        }
    }
}