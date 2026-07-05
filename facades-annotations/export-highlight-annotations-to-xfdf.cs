using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXfdf = "highlights.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor and bind the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Export only Highlight annotations
                string[] annotTypes = new string[] { "Highlight" };
                // Alternatively, use the enum overload:
                // AnnotationType[] annotTypes = new AnnotationType[] { AnnotationType.Highlight };

                // Export annotations from the first to the last page into an XFDF file
                using (FileStream fs = File.Create(outputXfdf))
                {
                    editor.ExportAnnotationsXfdf(fs, 1, doc.Pages.Count, annotTypes);
                }

                // Optional explicit close (using will dispose automatically)
                editor.Close();
            }
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}