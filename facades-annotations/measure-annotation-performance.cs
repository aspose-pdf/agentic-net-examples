using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf.Facades;          // facade types (required for binding)
using Aspose.Pdf.Annotations;      // annotation types
using Aspose.Pdf;                  // core PDF types (used with full namespace prefixes)

class AnnotationPerformanceLogger : IDisposable
{
    // Facade that works with PDF annotations
    private readonly PdfAnnotationEditor _editor;

    // Constructor binds the editor to an existing PDF file
    public AnnotationPerformanceLogger(string pdfPath)
    {
        _editor = new PdfAnnotationEditor();
        _editor.BindPdf(pdfPath);               // load the document
    }

    // Adds a TextAnnotation to the specified page and records the duration
    public void AddTextAnnotation(
        int pageNumber,
        double llx, double lly, double urx, double ury,
        string title,
        string contents,
        Color color)
    {
        var stopwatch = Stopwatch.StartNew();

        // Retrieve the target page (Aspose.Pdf uses 1‑based indexing)
        var page = _editor.Document.Pages[pageNumber];

        // Define the rectangle for the annotation
        var rect = new Rectangle(llx, lly, urx, ury);

        // Create the annotation and set its properties
        var annotation = new TextAnnotation(page, rect)
        {
            Title    = title,
            Contents = contents,
            Color    = color
        };

        // Add the annotation to the page
        page.Annotations.Add(annotation);

        stopwatch.Stop();
        Console.WriteLine($"AddTextAnnotation took {stopwatch.ElapsedMilliseconds} ms");
    }

    // Deletes an annotation by its name and records the duration
    public void DeleteAnnotationByName(string annotationName)
    {
        var stopwatch = Stopwatch.StartNew();

        _editor.DeleteAnnotation(annotationName);

        stopwatch.Stop();
        Console.WriteLine($"DeleteAnnotationByName took {stopwatch.ElapsedMilliseconds} ms");
    }

    // Modifies the Contents of an existing annotation and records the duration
    public void ModifyAnnotationContents(int pageNumber, string annotationName, string newContents)
    {
        var stopwatch = Stopwatch.StartNew();

        var page = _editor.Document.Pages[pageNumber];
        var annotation = page.Annotations.FindByName(annotationName);
        if (annotation != null)
        {
            annotation.Contents = newContents;
        }

        stopwatch.Stop();
        Console.WriteLine($"ModifyAnnotationContents took {stopwatch.ElapsedMilliseconds} ms");
    }

    // Saves the modified PDF and records the duration
    public void Save(string outputPath)
    {
        var stopwatch = Stopwatch.StartNew();

        _editor.Save(outputPath);   // PDF output; no SaveOptions needed

        stopwatch.Stop();
        Console.WriteLine($"Save took {stopwatch.ElapsedMilliseconds} ms");
    }

    // Properly releases resources held by the facade
    public void Dispose()
    {
        _editor.Close();   // closes the bound document
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Use the logger to perform annotation operations while measuring time
            using (var logger = new AnnotationPerformanceLogger(inputPdf))
            {
                // Add a text annotation on page 1
                logger.AddTextAnnotation(
                    pageNumber: 1,
                    llx: 100, lly: 500, urx: 300, ury: 550,
                    title: "Note",
                    contents: "Performance test annotation",
                    color: Color.Yellow);

                // Modify the annotation's contents (assuming the name is "Note")
                logger.ModifyAnnotationContents(
                    pageNumber: 1,
                    annotationName: "Note",
                    newContents: "Updated content");

                // Delete the annotation by name
                logger.DeleteAnnotationByName("Note");

                // Persist changes
                logger.Save(outputPdf);
            }

            Console.WriteLine($"Processed PDF saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
