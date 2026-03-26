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

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF that contains the annotation to be updated
            editor.BindPdf(inputPath);

            // Obtain the page object required by the TextAnnotation constructor
            Page page = editor.Document.Pages[1];
            // A rectangle must be supplied; its actual values are irrelevant when only the
            // Modified date is being updated, so a zero‑size rectangle is used.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the annotation with the required (Page, Rectangle) constructor
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Modified = DateTime.Now // set the modified date to the current system time
            };

            // Apply the modification to annotations on page 1 (start = 1, end = 1)
            editor.ModifyAnnotations(1, 1, annotation);

            // Save the updated PDF
            editor.Save(outputPath);
            Console.WriteLine($"Modified annotation saved to '{outputPath}'.");
        }
    }
}
