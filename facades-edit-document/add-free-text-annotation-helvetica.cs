using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);
        // Get the first page (or any target page)
        Page page = pdfDoc.Pages[1];

        // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a DefaultAppearance that defines font, size and color (Helvetica, 12pt, blue)
        // DefaultAppearance expects System.Drawing.Color, so we fully qualify it.
        var defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

        // Create the free‑text annotation using the constructor that accepts (Page, Rectangle, DefaultAppearance)
        FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, defaultAppearance)
        {
            Contents = "Sample free‑text annotation"
        };

        // Optional: set a simple border for the annotation (border color is controlled by the annotation's Color property)
        freeText.Border = new Border(freeText)
        {
            Width = 1,
            Style = BorderStyle.Solid
        };
        // Set border (and annotation) color – using Aspose.Pdf.Color to avoid ambiguity
        freeText.Color = Aspose.Pdf.Color.Black;

        // Add the annotation to the page
        page.Annotations.Add(freeText);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}