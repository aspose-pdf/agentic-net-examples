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
        // 1. Create a self‑contained PDF that contains a named annotation.
        //    The method returns the PDF as a byte[] and also outputs the name
        //    that was assigned to the annotation.
        // ---------------------------------------------------------------------
        byte[] pdfBytes = CreateSamplePdf(out string annotationName);

        // ---------------------------------------------------------------------
        // Example A – Delete the annotation using a **string literal**.
        // ---------------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the editor to the PDF that lives in memory.
            using (MemoryStream input = new MemoryStream(pdfBytes))
            {
                editor.BindPdf(input);
                // Delete the annotation by passing the literal value directly.
                editor.DeleteAnnotation(annotationName);

                // Save the result back to a file (or another stream).
                using (MemoryStream output = new MemoryStream())
                {
                    editor.Save(output);
                    File.WriteAllBytes("output_literal.pdf", output.ToArray());
                }
            }
        }

        // ---------------------------------------------------------------------
        // Example B – Delete the annotation using a **variable** that holds the name.
        // ---------------------------------------------------------------------
        string nameVariable = annotationName; // could come from any source
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            using (MemoryStream input = new MemoryStream(pdfBytes))
            {
                editor.BindPdf(input);
                // Delete the annotation by passing the variable.
                editor.DeleteAnnotation(nameVariable);

                using (MemoryStream output = new MemoryStream())
                {
                    editor.Save(output);
                    File.WriteAllBytes("output_variable.pdf", output.ToArray());
                }
            }
        }
    }

    /// <summary>
    /// Generates a minimal PDF that contains a single TextAnnotation.
    /// The annotation is given a unique name (GUID) so it can be addressed
    /// later with <c>PdfAnnotationEditor.DeleteAnnotation</c>.
    /// </summary>
    /// <param name="annotationName">The GUID that was assigned to the annotation.</param>
    /// <returns>A byte array representing the PDF document.</returns>
    static byte[] CreateSamplePdf(out string annotationName)
    {
        // Create a unique name for the annotation.
        annotationName = Guid.NewGuid().ToString();

        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // Define a rectangle where the annotation will appear.
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create a TextAnnotation and assign the generated name.
            TextAnnotation txt = new TextAnnotation(page, rect)
            {
                Title = "Sample",
                Contents = "This is a sample annotation",
                Name = annotationName,
                Open = true
            };
            page.Annotations.Add(txt);

            // Save the document to a memory stream and return the bytes.
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
