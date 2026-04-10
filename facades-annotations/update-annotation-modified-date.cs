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

        // Use PdfAnnotationEditor in a using block for deterministic disposal
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Get a reference to the first page (required for the TextAnnotation constructor)
            Page firstPage = editor.Document.Pages[1];
            // Create a zero‑size rectangle – the rectangle is not used when only modifying properties
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a TextAnnotation with the (Page, Rectangle) constructor and set the Modified date.
            TextAnnotation annot = new TextAnnotation(firstPage, rect)
            {
                Modified = DateTime.Now // set the modified date to the current system time
                // Optional: you can also set Title, Contents, Color, Subject, Open here
            };

            // Apply the modification to all pages (pages are 1‑based)
            int pageCount = editor.Document.Pages.Count;
            editor.ModifyAnnotations(1, pageCount, annot);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation modified date updated and saved to '{outputPath}'.");
    }
}
