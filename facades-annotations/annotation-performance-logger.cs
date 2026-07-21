using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class AnnotationPerformanceLogger
{
    private readonly PdfAnnotationEditor _editor;

    public AnnotationPerformanceLogger(string pdfPath)
    {
        // Initialize the facade and bind it to the existing PDF.
        _editor = new Aspose.Pdf.Facades.PdfAnnotationEditor();
        _editor.BindPdf(pdfPath);
    }

    // Expose the underlying Document for creating annotations.
    public Document Document => _editor.Document;

    private void Log(string operation, TimeSpan duration)
    {
        Console.WriteLine($"{operation} took {duration.TotalMilliseconds} ms");
    }

    // Adds an annotation to the specified page.
    public void AddAnnotation(Annotation annotation)
    {
        var sw = Stopwatch.StartNew();

        // The annotation is added to the page's annotation collection.
        // The page index is derived from the annotation's PageIndex after addition.
        Document doc = _editor.Document;
        // For demonstration, add to the first page if the annotation has no page assigned.
        if (annotation.PageIndex == 0)
        {
            doc.Pages[1].Annotations.Add(annotation);
        }
        else
        {
            doc.Pages[annotation.PageIndex].Annotations.Add(annotation);
        }

        sw.Stop();
        Log("AddAnnotation", sw.Elapsed);
    }

    // Deletes an annotation by its name.
    public void DeleteAnnotation(string name)
    {
        var sw = Stopwatch.StartNew();
        _editor.DeleteAnnotation(name);
        sw.Stop();
        Log("DeleteAnnotation", sw.Elapsed);
    }

    // Flattens all annotations in the document.
    public void FlattenAnnotations()
    {
        var sw = Stopwatch.StartNew();
        _editor.FlatteningAnnotations();
        sw.Stop();
        Log("FlattenAnnotations", sw.Elapsed);
    }

    // Saves the modified document.
    public void Save(string outputPath)
    {
        var sw = Stopwatch.StartNew();
        _editor.Save(outputPath);
        sw.Stop();
        Log("SaveDocument", sw.Elapsed);
    }

    public void Close()
    {
        _editor.Close();
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the logger with the source PDF.
        var logger = new AnnotationPerformanceLogger(inputPdf);

        // Create a rectangle for the annotation (llx, lly, urx, ury).
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a text annotation on the first page.
        TextAnnotation txtAnn = new TextAnnotation(logger.Document.Pages[1], rect)
        {
            Title = "Performance Note",
            Contents = "This annotation is added for performance monitoring.",
            Color = Aspose.Pdf.Color.Yellow
        };

        // Perform operations while measuring their duration.
        logger.AddAnnotation(txtAnn);
        logger.FlattenAnnotations();
        logger.Save(outputPdf);
        logger.Close();

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}