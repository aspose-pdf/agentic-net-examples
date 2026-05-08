using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Choose the page to which the annotation will be added (first page in this example).
        Page page = doc.Pages[1];

        // Define a rectangle where the annotation will appear (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        // Use the fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

        // Create a simple text annotation, set its opacity to 0.5 (50% transparent) and other properties.
        // NOTE: The constructor expects a Page object, not a page number.
        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Opacity = 0.5,
            Contents = "Sample annotation",
            Title = "Info"
        };

        // Add the annotation to the selected page.
        page.Annotations.Add(annotation);

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"Annotation opacity set to 0.5 and saved to '{outputPdf}'.");
    }
}
