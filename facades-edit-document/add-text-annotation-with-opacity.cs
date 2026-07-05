using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a simple text annotation on the first page
            Page page = doc.Pages[1];
            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Subtle visual effect",
                // Set opacity to 0.75 (range 0..1)
                Opacity  = 0.75
                // Note: Blend mode (e.g., Multiply) is not exposed directly on
                // the annotation API. If needed, it can be configured via PDF
                // graphics state when using low‑level drawing APIs.
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with annotation opacity set to 0.75: '{outputPdf}'");
    }
}