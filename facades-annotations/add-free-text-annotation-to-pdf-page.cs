using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF with the high‑level Document API (PdfContentEditor does not expose annotation creation methods).
        Document pdf = new Document(inputPath);

        // Define the rectangle for the free‑text annotation (llx, lly, urx, ury).
        Rectangle rect = new Rectangle(100, 500, 300, 550);

        // Define the default appearance (font, size, colour) for the annotation.
        DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

        // Create the free‑text annotation on page 1.
        FreeTextAnnotation freeText = new FreeTextAnnotation(pdf.Pages[1], rect, appearance)
        {
            Contents = "This is a free‑text annotation.",
            Color = Color.Yellow // optional background colour
        };
        // Optional border for the annotation.
        freeText.Border = new Border(freeText) { Width = 1 };

        // Add the annotation to the page's annotation collection.
        pdf.Pages[1].Annotations.Add(freeText);

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
