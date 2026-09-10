using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationType enum

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor and bind the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Specify that only Highlight annotations should be exported
                AnnotationType[] types = new AnnotationType[] { AnnotationType.Highlight };

                // Export annotations from the first to the last page into an XFDF file
                using (FileStream xfdfStream = File.Create(outputXfdf))
                {
                    // Pages are 1‑based in Aspose.Pdf
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, doc.Pages.Count, types);
                }
            }

            // No modifications to the PDF itself, so no need to save the document
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}