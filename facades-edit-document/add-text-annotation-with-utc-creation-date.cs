using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the source PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Create a Text (sticky‑note) annotation on the first page
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
        TextAnnotation txtAnn = new TextAnnotation(doc);
        txtAnn.Rect = rect;
        txtAnn.Title    = "Note";
        txtAnn.Contents = "Annotation with UTC creation date";
        txtAnn.CreationDate = DateTime.UtcNow;   // set creation timestamp to current UTC time

        // Add the annotation to the page
        doc.Pages[1].Annotations.Add(txtAnn);

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Annotation added. Output saved to '{outputPdf}'.");
    }
}