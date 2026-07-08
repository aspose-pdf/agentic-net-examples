using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the document to obtain a Page instance – required for the TextAnnotation ctor.
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // TextAnnotation does not have a parameter‑less constructor. Use the (Page, Rectangle) overload.
        // An empty rectangle is sufficient when the annotation is only a template for ModifyAnnotations.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
        TextAnnotation template = new TextAnnotation(page, rect)
        {
            Subject = "ReviewNote"
        };

        // Bind the PDF to the annotation editor and apply the template to annotations on page 1.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.ModifyAnnotations(start: 1, end: 1, annotation: template);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Subject assigned and saved to '{outputPath}'.");
    }
}
