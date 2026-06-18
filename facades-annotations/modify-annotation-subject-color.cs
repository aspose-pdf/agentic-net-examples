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

        // Load the PDF document (needed to obtain a Page object for the annotation constructor)
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1]; // 1‑based indexing

        // Create a rectangle for the annotation (position and size can be adjusted as needed)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

        // Instantiate TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation newAnnotation = new TextAnnotation(page, rect)
        {
            Subject = "Updated Subject",
            Color = Aspose.Pdf.Color.Blue
        };

        // Initialize the annotation editor facade and bind the same PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Modify the first annotation (index 1) on page 1 with the new annotation
            editor.ModifyAnnotations(1, 1, newAnnotation);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
