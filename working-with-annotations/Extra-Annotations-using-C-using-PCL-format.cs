using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // Add a simple Text annotation on the first page
            // -------------------------------------------------
            Page page1 = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation first, then set its properties (including Border)
            TextAnnotation textAnn = new TextAnnotation(page1, textRect);
            textAnn.Title    = "Note";
            textAnn.Contents = "This is an extra text annotation.";
            textAnn.Color    = Aspose.Pdf.Color.Yellow;
            // Set border after the annotation instance exists
            textAnn.Border   = new Border(textAnn) { Width = 1 };
            page1.Annotations.Add(textAnn);

            // -------------------------------------------------
            // Add a Polygon annotation on the second page
            // -------------------------------------------------
            if (doc.Pages.Count >= 2)
            {
                Page page2 = doc.Pages[2];

                // Define polygon vertices (example triangle)
                Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(150, 600),
                    new Aspose.Pdf.Point(250, 700),
                    new Aspose.Pdf.Point(350, 600)
                };

                // Rectangle that bounds the polygon (required by constructor)
                Aspose.Pdf.Rectangle polyRect = new Aspose.Pdf.Rectangle(140, 590, 360, 710);

                // Create the annotation first, then set its properties
                PolygonAnnotation polyAnn = new PolygonAnnotation(page2, polyRect, vertices);
                polyAnn.Color         = Aspose.Pdf.Color.Blue;
                polyAnn.InteriorColor = Aspose.Pdf.Color.LightBlue;
                polyAnn.Contents      = "Sample polygon annotation.";
                polyAnn.Border        = new Border(polyAnn) { Width = 2 };
                page2.Annotations.Add(polyAnn);
            }

            // -------------------------------------------------
            // Save the modified document.
            // NOTE: Aspose.Pdf does NOT support saving to PCL format.
            // PCL is input‑only (via PclLoadOptions). Attempting to use
            // a non‑existent PclSaveOptions would cause a compile error.
            // Therefore we save the result as PDF.
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
        Console.WriteLine("Saving to PCL format is not supported by Aspose.Pdf; only PDF output is possible.");
    }
}
