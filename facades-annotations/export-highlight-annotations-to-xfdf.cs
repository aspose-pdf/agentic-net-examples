using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade API for annotation handling
using Aspose.Pdf.Annotations;      // Contains AnnotationType enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // Source PDF containing annotations
        const string outputXfdf = "highlights.xfdf"; // Destination XFDF file

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);

            // Define the annotation types to export – only Highlight annotations
            AnnotationType[] types = new AnnotationType[] { AnnotationType.Highlight };

            // Determine the page range (export from first to last page)
            int startPage = 1;                                 // Aspose.Pdf uses 1‑based indexing
            int endPage   = editor.Document.Pages.Count;       // Total number of pages in the PDF

            // Create the XFDF output stream and export the selected annotations
            using (FileStream xfdfStream = File.Create(outputXfdf))
            {
                editor.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, types);
            }
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}