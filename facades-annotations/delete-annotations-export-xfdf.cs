using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare a sample PDF with a Text annotation (so the file always exists
        //    when the demo runs). This eliminates the FileNotFoundException that
        //    occurred in the original code.
        // ---------------------------------------------------------------------
        string inputPdf = Path.Combine(Path.GetTempPath(), "sample_input.pdf");
        CreateSamplePdfWithTextAnnotation(inputPdf);

        string outputPdf = Path.Combine(Path.GetTempPath(), "sample_output.pdf");
        string xfdfFile = Path.Combine(Path.GetTempPath(), "remaining_annotations.xfdf");

        // ---------------------------------------------------------------------
        // 2. Delete all annotations of type "Text" and export the remaining ones
        //    to an XFDF file.
        // ---------------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Delete every annotation whose type name is "Text".
            // The method accepts the annotation type as a string (e.g., "Text",
            // "Link", "Highlight", etc.).
            editor.DeleteAnnotations("Text");

            // Save the PDF after the deletion.
            editor.Save(outputPdf);

            // Export the *remaining* annotations (if any) to an XFDF file.
            using (FileStream xfdfStream = File.Create(xfdfFile))
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Input PDF   : {inputPdf}");
        Console.WriteLine($"Output PDF  : {outputPdf}");
        Console.WriteLine($"XFDF export : {xfdfFile}");
        Console.WriteLine("Text annotations deleted and remaining annotations exported to XFDF.");
    }

    /// <summary>
    /// Creates a one‑page PDF containing a single Text annotation.
    /// This helper guarantees that the demo has a valid source file.
    /// </summary>
    private static void CreateSamplePdfWithTextAnnotation(string filePath)
    {
        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a blank page.
            Page page = doc.Pages.Add();

            // Create a Text annotation (the type we will later delete).
            TextAnnotation txtAnn = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 300, 650))
            {
                Title = "Sample",
                Subject = "Demo",
                Contents = "This is a sample text annotation."
            };

            // Add the annotation to the page.
            page.Annotations.Add(txtAnn);

            // Save the PDF to the supplied path.
            doc.Save(filePath);
        }
    }
}
