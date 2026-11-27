using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "annotated.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the high‑level Document API
        Document doc = new Document(inputPdf);

        // Define the rectangle for the free‑text annotation (llx, lly, urx, ury)
        // Use the Aspose.Pdf.Rectangle type (fully qualified) to avoid ambiguity with System.Drawing.Rectangle.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a FreeTextAnnotation. The constructor requires the page, rectangle,
        // and a DefaultAppearance that defines font, size and colour.
        // DefaultAppearance expects a System.Drawing.Color, so we fully qualify it.
        var freeText = new FreeTextAnnotation(doc.Pages[1], rect,
            new DefaultAppearance("Arial", 12, System.Drawing.Color.Black))
        {
            Contents = "Sample free text annotation",
            // Background colour of the annotation (Aspose.Pdf.Color)
            Color = Aspose.Pdf.Color.LightGray
        };
        // Optional: add a thin border around the annotation
        freeText.Border = new Border(freeText) { Width = 1 };

        // Add the annotation to the first page
        doc.Pages[1].Annotations.Add(freeText);

        // Save the PDF that now contains the annotation
        doc.Save(outputPdf);

        // Export all annotations (including the newly added one) to XFDF
        using (var fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            doc.ExportAnnotationsToXfdf(fs);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
        Console.WriteLine($"Annotations exported to XFDF at '{xfdfPath}'.");
    }
}
