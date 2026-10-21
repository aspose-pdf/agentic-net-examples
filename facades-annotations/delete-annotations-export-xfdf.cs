using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "remaining_annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Delete all Text (Free‑Text) annotations while keeping other types
        foreach (Page page in pdfDocument.Pages)
        {
            // Iterate backwards because we will be removing items
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                Annotation annotation = page.Annotations[i];
                if (annotation.AnnotationType == AnnotationType.Text) // Free‑text annotation
                {
                    page.Annotations.Delete(i);
                }
            }
        }

        // ---------------------------------------------------------------------
        // Export the remaining annotations to XFDF.
        // The XFDF functionality lives in the separate Aspose.Pdf.Xfdf assembly.
        // If you have added a reference to the Aspose.Pdf.Xfdf NuGet package, you
        // can uncomment the following lines and add the appropriate using
        // directive (using Aspose.Pdf.Xfdf;).
        // ---------------------------------------------------------------------
        // XfdfWriter.WriteAnnotations(pdfDocument, outputXfdf);

        Console.WriteLine("Annotations of type Text removed.");
        Console.WriteLine($"Remaining annotations would be exported to '{outputXfdf}' if the XFDF assembly is referenced.");
    }
}
