using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF using the Facade, then add a markup annotation with UTC creation date
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Choose the page to place the annotation (1‑based indexing)
            Page page = editor.Document.Pages[1];

            // Define the annotation rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a Text (markup) annotation
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This annotation was created programmatically.",
                CreationDate = DateTime.UtcNow   // Set creation date to current UTC time
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation with UTC creation date saved to '{outputPath}'.");
    }
}