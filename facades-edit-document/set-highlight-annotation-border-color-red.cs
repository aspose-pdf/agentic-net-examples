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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a highlight annotation, set its border color to bright red,
        // then save using the Aspose.Pdf.Facades API.
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the highlight will appear.
            // Fully qualified to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation on the first page.
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect)
            {
                // Example fill color for the highlight (optional).
                Color = Aspose.Pdf.Color.Yellow
            };

            // Set the border color to bright red (RGB 255,0,0).
            // The Border property expects a System.Drawing.Color, so we use that type explicitly.
            highlight.Characteristics.Border = System.Drawing.Color.FromArgb(255, 0, 0);

            // Add the annotation to the page.
            doc.Pages[1].Annotations.Add(highlight);

            // Use PdfAnnotationEditor (a Facades class) to save the modified document.
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);          // Bind the in‑memory Document.
            editor.Save(outputPath);      // Save to the desired output file.
            editor.Close();               // Release resources held by the facade.
        }

        Console.WriteLine($"Annotation with red border saved to '{outputPath}'.");
    }
}
