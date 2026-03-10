using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // required for DefaultAppearance

class FloatingAnnotationDemo
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Use 1‑based page indexing (page-indexing-one-based rule)
            Page page = doc.Pages[1];

            // ---------- 1. Add a floating text annotation (FreeTextAnnotation) ----------
            // Define the rectangle where the annotation will appear.
            // Fully qualify Rectangle to avoid ambiguity (rectangle-disambiguation rule)
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // DefaultAppearance constructor expects System.Drawing.Color (default-appearance-font-is-readonly-and-no-fontcolor rule)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation on the page.
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, textRect, appearance)
            {
                Contents = "This is a floating text annotation.",
                Color    = Aspose.Pdf.Color.Yellow   // background color of the annotation box
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(freeText);

            // ---------- 2. Add a floating image annotation (StampAnnotation) ----------
            // Define the rectangle for the image annotation.
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(350, 600, 550, 800);

            // Create the StampAnnotation.
            StampAnnotation stamp = new StampAnnotation(page, imageRect);

            // Load an image from file and assign it to the annotation.
            // StampAnnotation.Image expects a Stream, not Aspose.Pdf.Image (image-constructor-and-watermark-annotation rule)
            stamp.Image = File.OpenRead("sample_image.png");

            // Optional: set a border color for visual distinction.
            stamp.Border = new Border(stamp) { Width = 1 };
            stamp.Color  = Aspose.Pdf.Color.LightGray;

            // Add the image annotation to the page.
            page.Annotations.Add(stamp);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
    }
}
