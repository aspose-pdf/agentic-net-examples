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

        // Bind the PDF document to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Obtain a page reference (required for TextAnnotation constructor)
            Page firstPage = editor.Document.Pages[1];
            // Define a rectangle – size can be zero if the annotation is only used for metadata update
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a TextAnnotation with the Modified date set to the current time
            TextAnnotation annotation = new TextAnnotation(firstPage, rect)
            {
                Modified = DateTime.Now
                // Additional properties (Title, Contents, etc.) can be set here if needed
            };

            // Apply the modification to all pages (1‑based indexing)
            int startPage = 1;
            int endPage = editor.Document.Pages.Count;
            editor.ModifyAnnotations(startPage, endPage, annotation);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated annotation saved to '{outputPath}'.");
    }
}
