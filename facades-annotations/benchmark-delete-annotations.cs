using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

public class BenchmarkDeleteAnnotations
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputAllPath = "output_all.pdf";
        const string outputSinglePath = "output_single.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Benchmark DeleteAnnotations (delete all annotations)
        using (Document docAll = new Document(inputPath))
        {
            PdfAnnotationEditor editorAll = new PdfAnnotationEditor();
            editorAll.BindPdf(docAll);
            Stopwatch swAll = new Stopwatch();
            swAll.Start();
            editorAll.DeleteAnnotations();
            swAll.Stop();
            long elapsedAll = swAll.ElapsedMilliseconds;
            editorAll.Save(outputAllPath);
            editorAll.Close();
            Console.WriteLine("DeleteAnnotations elapsed (ms): " + elapsedAll);
        }

        // Benchmark DeleteAnnotation (delete a single annotation by name)
        using (Document docSingle = new Document(inputPath))
        {
            // Retrieve the name of the first annotation on the first page
            Annotation firstAnnotation = docSingle.Pages[1].Annotations[1];
            string annotationName = firstAnnotation.Name;

            PdfAnnotationEditor editorSingle = new PdfAnnotationEditor();
            editorSingle.BindPdf(docSingle);
            Stopwatch swSingle = new Stopwatch();
            swSingle.Start();
            editorSingle.DeleteAnnotation(annotationName);
            swSingle.Stop();
            long elapsedSingle = swSingle.ElapsedMilliseconds;
            editorSingle.Save(outputSinglePath);
            editorSingle.Close();
            Console.WriteLine("DeleteAnnotation elapsed (ms): " + elapsedSingle);
        }
    }
}
