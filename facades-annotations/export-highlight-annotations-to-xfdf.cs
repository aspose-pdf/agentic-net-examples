using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "highlights.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor with the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Specify that only Highlight annotations should be exported
            AnnotationType[] types = new AnnotationType[] { AnnotationType.Highlight };

            // Export the selected annotations to an XFDF file
            using (FileStream xfdfStream = File.Create(outputXfdf))
            {
                // Export from the first to the last page of the document
                editor.ExportAnnotationsXfdf(xfdfStream, 1, doc.Pages.Count, types);
            }

            // Release resources held by the editor
            editor.Close();
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}