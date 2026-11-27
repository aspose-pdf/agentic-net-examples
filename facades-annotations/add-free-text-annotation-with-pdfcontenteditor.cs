using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // PdfContentEditor lives here

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

        // Load the PDF with PdfContentEditor (bind) – this gives us access to the underlying Document.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        Document doc = editor.Document; // underlying high‑level Document object

        // Define a rectangle for the free‑text annotation on page 2 (llx, lly, urx, ury).
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Build the required DefaultAppearance (font name, size, colour).
        DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

        // Create the free‑text annotation (page, rectangle, appearance).
        FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[2], rect, appearance)
        {
            // The visible text of the annotation.
            Contents = "Sample free text annotation",
            // Background colour of the annotation (Aspose.Pdf.Color).
            Color = Aspose.Pdf.Color.Yellow,
        };

        // Optional border – the Border class is constructed with the parent annotation.
        freeText.Border = new Border(freeText) { Width = 1 };

        // Add the annotation to page 2.
        doc.Pages[2].Annotations.Add(freeText);

        // Save the modified PDF via the editor (or directly via the Document).
        editor.Save(outputPath);
        // Alternatively: doc.Save(outputPath);

        Console.WriteLine($"Free text annotation added and saved to '{outputPath}'.");
    }
}