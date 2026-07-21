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

        // Load the document to obtain a Page object (required for TextAnnotation ctor)
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // Create a dummy rectangle – the actual position/size is not used when only
        // changing properties like Subject or Color via ModifyAnnotations.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

        // Instantiate TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation newAnnotation = new TextAnnotation(page, rect)
        {
            Subject = "New Subject",
            Color = Aspose.Pdf.Color.Red
        };

        // Initialize the annotation editor and bind the PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Apply the changes to the first annotation on page 1 (annotation index is 1‑based)
        editor.ModifyAnnotations(1, 1, newAnnotation);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
