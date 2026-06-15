using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor and bind it to the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Specify the annotation type to export – only "Highlight"
                string[] annotTypes = new string[] { "Highlight" };

                // Export annotations from the first to the last page into an XFDF file
                using (FileStream xfdfStream = File.Create(outputXfdf))
                {
                    editor.ExportAnnotationsXfdf(xfdfStream, 1, doc.Pages.Count, annotTypes);
                }
            }
        }

        Console.WriteLine($"Highlight annotations exported to '{outputXfdf}'.");
    }
}