using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXfdfPath = "visibility_state.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the annotation editor facade and bind the document
            PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
            annotationEditor.BindPdf(pdfDoc);

            // Export all annotations (including their visibility state) to XFDF (XML) via a stream
            using (FileStream outputStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                annotationEditor.ExportAnnotationsToXfdf(outputStream);
            }

            // No changes are made to the PDF itself, so no need to save it
        }

        Console.WriteLine($"Conditional visibility state exported to '{outputXfdfPath}'.");
    }
}