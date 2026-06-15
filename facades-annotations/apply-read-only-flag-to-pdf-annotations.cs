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

        // Load the document to obtain a Page object (PdfAnnotationEditor does not expose GetPage)
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1]; // first page (1‑based index)

        // A rectangle is required by the constructor; its actual size is irrelevant when only flags are changed
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

        // Create a TextAnnotation template and set the ReadOnly flag
        TextAnnotation template = new TextAnnotation(page, rect)
        {
            Flags = AnnotationFlags.ReadOnly
        };

        // Bind the PDF to the annotation editor and apply the flag to annotations on the desired page range
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.ModifyAnnotations(1, 1, template);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Read‑only flag applied and saved to '{outputPath}'.");
    }
}
