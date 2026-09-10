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
        const string outputPath = "locked_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text annotation on the first page
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This annotation is locked.",
                Color = Aspose.Pdf.Color.Yellow,
                // Apply the Locked flag to prevent user modifications
                Flags = AnnotationFlags.Locked
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Use the Facades API (PdfAnnotationEditor) to save the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"PDF saved with locked annotation: {outputPath}");
    }
}