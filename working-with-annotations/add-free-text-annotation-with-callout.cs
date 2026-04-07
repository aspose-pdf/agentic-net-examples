using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using System.Drawing; // Required for DefaultAppearance color

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

        // Document lifecycle – wrap in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance via constructor (System.Drawing.Color required)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation with the appearance
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Important note with callout",
                Color    = Aspose.Pdf.Color.Yellow, // Border color of the annotation
                Opacity  = 0.8
            };

            // Callout line – exactly three points: start (inside), knee (bend), end (target)
            ft.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525), // start point inside the annotation
                new Aspose.Pdf.Point(200, 600), // knee point
                new Aspose.Pdf.Point(250, 650)  // end point on the page
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}