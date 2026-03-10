using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // <-- Added namespace for PdfAnnotationEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document using the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Example: add a highlight annotation on page 1
        // Define the rectangle (coordinates are in points; 1 point = 1/72 inch)
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 520; // upper‑right Y

        // Create the highlight annotation
        HighlightAnnotation highlight = new HighlightAnnotation(
            doc.Pages[1],
            new Aspose.Pdf.Rectangle(llx, lly, urx, ury));

        // Set the highlight colour (cross‑platform Aspose colour)
        highlight.Color = Aspose.Pdf.Color.Yellow;

        // Define QuadPoints so the highlight covers the exact rectangle area.
        // QuadPoints expects an array of Aspose.Pdf.Point objects.
        highlight.QuadPoints = new Aspose.Pdf.Point[]
        {
            new Aspose.Pdf.Point(llx, ury), // top‑left
            new Aspose.Pdf.Point(urx, ury), // top‑right
            new Aspose.Pdf.Point(urx, lly), // bottom‑right
            new Aspose.Pdf.Point(llx, lly)  // bottom‑left
        };

        // Add the annotation to the page
        doc.Pages[1].Annotations.Add(highlight);

        // Save the modified PDF using the facade's Save method
        editor.Save(outputPath);

        // Clean up resources
        editor.Close();

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}
