using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputAll = "output_all.pdf";
        const string outputSingle = "output_single.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Benchmark DeleteAnnotations (remove all annotations)
        Stopwatch swAll = Stopwatch.StartNew();
        using (Document doc = new Document(inputPath))
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.DeleteAnnotations(); // delete all annotations
            editor.Save(outputAll);
        }
        swAll.Stop();
        Console.WriteLine($"DeleteAnnotations elapsed: {swAll.ElapsedMilliseconds} ms");

        // Retrieve the name of the first annotation for single deletion benchmark
        string firstAnnotName = null;
        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count > 0 && doc.Pages[1].Annotations.Count > 0)
            {
                // Annotations collection uses 1‑based indexing
                firstAnnotName = doc.Pages[1].Annotations[1].Name;
            }
        }

        if (string.IsNullOrEmpty(firstAnnotName))
        {
            Console.WriteLine("No annotations found to delete individually.");
            return;
        }

        // Benchmark DeleteAnnotation (remove a single annotation by name)
        Stopwatch swSingle = Stopwatch.StartNew();
        using (Document doc = new Document(inputPath))
        {
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.DeleteAnnotation(firstAnnotName); // delete specific annotation
            editor.Save(outputSingle);
        }
        swSingle.Stop();
        Console.WriteLine($"DeleteAnnotation elapsed: {swSingle.ElapsedMilliseconds} ms");
    }
}