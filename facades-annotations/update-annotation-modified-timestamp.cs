using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document to obtain the page count (required for the editor range).
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // Aspose.Pdf uses 1‑based indexing.

            // Initialize the PdfAnnotationEditor facade and bind the source PDF.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPdf);

            // Create a prototype annotation with the desired Modified timestamp.
            // TextAnnotation does not have a parameter‑less constructor; use the (Page, Rectangle) overload.
            Page dummyPage = doc.Pages[1];
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation prototype = new TextAnnotation(dummyPage, dummyRect)
            {
                Modified = DateTime.Now
            };

            // Apply the modification to all pages. The editor will update the
            // Modified property of every TextAnnotation found between start and end pages.
            editor.ModifyAnnotations(1, pageCount, prototype);

            // Save the updated PDF.
            editor.Save(outputPdf);

            // Release resources held by the facade.
            editor.Close();
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPdf}'.");
    }
}
