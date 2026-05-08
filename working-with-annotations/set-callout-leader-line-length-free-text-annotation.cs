using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance for the annotation text (font, size, color)
            // Note: DefaultAppearance constructor expects System.Drawing.Color for the third argument
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation on the page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Visible text inside the annotation
                Contents = "Sample free‑text annotation with a 20‑point leader line."
            };

            // Set up the callout line (leader line) points
            // Point[0] – start point inside the annotation rectangle
            // Point[1] – knee point (offset by 20 points horizontally)
            // Point[2] – end point (where the line points to)
            freeText.Callout = new Point[]
            {
                new Point(200, 525),   // start (center of the rectangle)
                new Point(220, 525),   // knee – 20 points to the right
                new Point(250, 525)    // end – further right
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with 20‑point leader line saved to '{outputPath}'.");
    }
}