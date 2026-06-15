using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class AnnotationOperationLogger
{
    /// <summary>
    /// Executes the supplied <paramref name="action"/> while measuring its execution time.
    /// The duration (in milliseconds) is written to the console together with the supplied
    /// <paramref name="operationName"/>. This class acts as a simple logging interceptor for
    /// annotation‑related operations.
    /// </summary>
    public static void Execute(string operationName, Action action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));
        var sw = Stopwatch.StartNew();
        try
        {
            action();
        }
        finally
        {
            sw.Stop();
            Console.WriteLine($"{operationName} duration: {sw.ElapsedMilliseconds} ms");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string importPdf = "source.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the source PDF.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // -------------------------------------------------
        // 1. Add a TextAnnotation to the first page.
        // -------------------------------------------------
        AnnotationOperationLogger.Execute("Add TextAnnotation", () =>
        {
            Page firstPage = editor.Document.Pages[1]; // 1‑based indexing
            Rectangle rect = new Rectangle(100, 500, 300, 550);
            TextAnnotation textAnnot = new TextAnnotation(firstPage, rect)
            {
                Title = "Note Title",
                Contents = "This is a sample text annotation.",
                Color = Color.Yellow,
                Open = true
            };
            firstPage.Annotations.Add(textAnnot);
        });

        // -------------------------------------------------
        // 2. Modify the annotation using ModifyAnnotations.
        // -------------------------------------------------
        AnnotationOperationLogger.Execute("ModifyAnnotations", () =>
        {
            // For modification we still need a TextAnnotation instance, but the page/rectangle
            // are not used by the editor – they are only required to satisfy the constructor.
            Page dummyPage = editor.Document.Pages[1];
            Rectangle dummyRect = new Rectangle(0, 0, 0, 0);
            TextAnnotation modifiedAnnot = new TextAnnotation(dummyPage, dummyRect)
            {
                Modified = DateTime.Now,
                Title = "Updated Title",
                Contents = "Updated contents of the annotation.",
                Color = Color.Red,
                Subject = "Updated Subject",
                Open = true
            };
            // Apply modification to pages 1 through 1.
            editor.ModifyAnnotations(1, 1, modifiedAnnot);
        });

        // -------------------------------------------------
        // 3. Delete all Text annotations.
        // -------------------------------------------------
        AnnotationOperationLogger.Execute("DeleteAnnotations (Text)", () =>
        {
            // The annotation type name is case‑sensitive; "Text" corresponds to TextAnnotation.
            editor.DeleteAnnotations("Text");
        });

        // -------------------------------------------------
        // 4. Import annotations from another PDF (if it exists).
        // -------------------------------------------------
        if (File.Exists(importPdf))
        {
            AnnotationOperationLogger.Execute("ImportAnnotations", () =>
            {
                // Import all annotation types from the source PDF.
                editor.ImportAnnotations(new string[] { importPdf });
            });
        }
        else
        {
            Console.WriteLine($"Import source PDF not found: {importPdf} (skipping import).");
        }

        // -------------------------------------------------
        // 5. Save the modified document.
        // -------------------------------------------------
        AnnotationOperationLogger.Execute("Save", () =>
        {
            editor.Save(outputPdf);
        });

        // Clean up.
        editor.Close();
        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}
