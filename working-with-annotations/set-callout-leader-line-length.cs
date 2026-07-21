using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // for DefaultAppearance
using System.Drawing;          // needed for DefaultAppearance color

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a DefaultAppearance for the free‑text annotation
            // Note: the constructor expects System.Drawing.Color for the text color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Define the rectangle where the annotation will be placed
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the FreeTextAnnotation
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Important note",
                Color    = Aspose.Pdf.Color.Yellow,   // background color of the annotation
                Title    = "Note"
            };

            // Set the callout points.
            // The first segment (leader line) will be 20 points long.
            // Points are defined in user space coordinates (points).
            ft.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525), // start point (inside the annotation)
                new Aspose.Pdf.Point(170, 525), // knee point – 20 points away horizontally
                new Aspose.Pdf.Point(200, 560)  // end point (pointing to the target)
            };

            // Add the annotation to the page
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with 20‑point leader line saved to '{outputPath}'.");
    }
}