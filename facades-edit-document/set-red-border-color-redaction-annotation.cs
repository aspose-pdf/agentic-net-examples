using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_redacted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Facade and bind the existing PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Define the rectangle for the annotation (coordinates in points)
        // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a RedactionAnnotation on page 1
        RedactionAnnotation redaction = new RedactionAnnotation(doc.Pages[1], rect);

        // Set the border color to bright red (RGB 255,0,0)
        redaction.BorderColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0); // or Aspose.Pdf.Color.Red

        // Optionally set a fill color to visualize the area (semi‑transparent red)
        redaction.FillColor = Aspose.Pdf.Color.FromRgb(1.0, 0.8, 0.8);
        redaction.Opacity = 0.5;

        // Add the annotation to the page
        doc.Pages[1].Annotations.Add(redaction);

        // Save the modified PDF via the Facade
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Annotation with red border saved to '{outputPath}'.");
    }
}