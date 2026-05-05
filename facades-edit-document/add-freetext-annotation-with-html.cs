using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for DefaultAppearance color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the annotation editor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create a DefaultAppearance (font name, size, System.Drawing.Color)
                DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

                // Create a FreeTextAnnotation on page 1
                FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
                {
                    // Optional plain text shown in the annotation area
                    Contents = "Rich Text Annotation",

                    // HTML / rich text rendered inside the annotation
                    RichText = "<b>Bold</b> <i>Italic</i> <u>Underline</u> <font color=\"red\">Red Text</font>",

                    // Visual styling
                    Color = Aspose.Pdf.Color.Blue,
                    Opacity = 0.8
                };

                // Add the annotation to the page
                doc.Pages[1].Annotations.Add(freeText);

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Free‑text annotation with HTML content added. Saved to '{outputPath}'.");
    }
}