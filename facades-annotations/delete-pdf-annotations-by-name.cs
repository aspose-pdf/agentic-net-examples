using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a sample PDF that contains a named annotation.
        //    The PDF is kept in memory so the example does not depend on an
        //    external file on disk.
        // ---------------------------------------------------------------------
        byte[] pdfBytes = CreateSamplePdfWithAnnotation(out string annotationName);

        // ---------------------------------------------------------------------
        // Example A – Delete the annotation using a **string literal**.
        // ---------------------------------------------------------------------
        using (var editor = new PdfAnnotationEditor())
        {
            // Bind the PDF from a memory stream (no file I/O required).
            using (var input = new MemoryStream(pdfBytes))
            {
                editor.BindPdf(input);
                // The literal can be written directly, but we reuse the generated name
                // to keep the example functional.
                editor.DeleteAnnotation(annotationName);

                // Save the result to a new file.
                using (var output = new MemoryStream())
                {
                    editor.Save(output);
                    File.WriteAllBytes("output_literal.pdf", output.ToArray());
                }
            }
        }

        // ---------------------------------------------------------------------
        // Example B – Delete the annotation using a **variable** that holds the name.
        // ---------------------------------------------------------------------
        string nameFromVariable = annotationName; // could be any string variable
        using (var editor = new PdfAnnotationEditor())
        {
            using (var input = new MemoryStream(pdfBytes))
            {
                editor.BindPdf(input);
                editor.DeleteAnnotation(nameFromVariable);

                using (var output = new MemoryStream())
                {
                    editor.Save(output);
                    File.WriteAllBytes("output_variable.pdf", output.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// Creates a minimal PDF document that contains a single TextAnnotation.
    /// The annotation is given a unique name (GUID) which is returned via the
    /// out parameter so the caller can later delete it.
    /// </summary>
    private static byte[] CreateSamplePdfWithAnnotation(out string annotationName)
    {
        // Generate a unique name for the annotation.
        annotationName = Guid.NewGuid().ToString();

        // Build the PDF in memory.
        var doc = new Document();
        var page = doc.Pages.Add();

        // Define a rectangle where the annotation will appear.
        var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

        // Create a text annotation and assign the generated name.
        var textAnnotation = new TextAnnotation(page, rect)
        {
            Title = "Sample",
            Contents = "This is a sample annotation",
            Name = annotationName // <-- this is the identifier used by DeleteAnnotation
        };
        page.Annotations.Add(textAnnotation);

        // Save the document to a byte array.
        using (var ms = new MemoryStream())
        {
            doc.Save(ms);
            return ms.ToArray();
        }
    }
}
