using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain a Page object required by the TextAnnotation constructor
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // Initialize the annotation editor and bind the source PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Create a new TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation changedAnnot = new TextAnnotation(page, new Rectangle(0, 0, 0, 0))
        {
            Subject = "Updated Subject",
            Color   = Aspose.Pdf.Color.Red,
            Modified = DateTime.Now // optional: update modification date
        };

        // Apply the changes to annotations on page 1 (adjust range as needed)
        editor.ModifyAnnotations(start: 1, end: 1, annotation: changedAnnot);

        // Save the modified PDF
        editor.Save(outputPath);
    }
}
