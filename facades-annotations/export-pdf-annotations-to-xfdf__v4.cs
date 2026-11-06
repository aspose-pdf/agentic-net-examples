using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            annotationEditor.BindPdf(inputPdf);

            // Determine the page range to export (all pages)
            int startPage = 1;
            int endPage = annotationEditor.Document.Pages.Count;

            // Export all annotations to an XFDF file via a FileStream
            using (FileStream outputStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                // Use the overload that accepts a string[]; cast null to string[] to avoid ambiguity
                annotationEditor.ExportAnnotationsXfdf(outputStream, startPage, endPage, (string[])null);
            }
        }

        Console.WriteLine($"Annotations successfully exported to '{outputXfdf}'.");
    }
}
