using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain a Page instance for the annotation constructor
        Document doc = new Document(inputPath);
        Page firstPage = doc.Pages[1];

        // Create the TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation customAnnot = new TextAnnotation(firstPage,
            new Aspose.Pdf.Rectangle(100, 500, 300, 550))
        {
            Flags = AnnotationFlags.Print | AnnotationFlags.Locked,
            Title = "Custom Flag",
            Contents = "Annotation with custom flags",
            Color = Aspose.Pdf.Color.Yellow
        };

        // Initialize the annotation editor and bind the same document instance
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(doc);

            // Apply the modified annotation to pages 1 through 2
            editor.ModifyAnnotations(1, 2, customAnnot);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}