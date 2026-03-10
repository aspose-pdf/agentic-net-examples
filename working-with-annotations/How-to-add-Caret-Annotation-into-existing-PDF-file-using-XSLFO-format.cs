using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_caret.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Select the page where the annotation will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the caret annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the selected page
            CaretAnnotation caret = new CaretAnnotation(page, rect)
            {
                // Optional visual and descriptive properties
                Contents = "Sample caret annotation",
                Color    = Aspose.Pdf.Color.Blue
                // Symbol can be set if needed, e.g. Symbol = CaretAnnotationSymbol.None
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(caret);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPdf}'.");
    }
}