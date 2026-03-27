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

        // Load the document to obtain a Page object required by TextAnnotation constructor
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];
        // Define a rectangle for the annotation (position and size). Adjust coordinates as needed.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

        // Create the TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Subject = "Updated Subject",
            Color = Aspose.Pdf.Color.Blue
        };

        // Use PdfAnnotationEditor to modify annotations on page 1 (start = 1, end = 1)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        editor.ModifyAnnotations(1, 1, annotation);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}
