using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the PdfAnnotationEditor facade to bind the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Create a text annotation on the first page
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Contents = "Sample annotation with thick border",
                Color    = Aspose.Pdf.Color.Yellow   // annotation fill color
            };

            // Set a custom border thickness of three points
            annotation.Border = new Border(annotation) { Width = 3 };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF using the facade
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}