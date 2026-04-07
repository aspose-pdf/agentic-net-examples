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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a FreeTextAnnotation on the first page
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
            {
                Contents = "Callout example",
                Color = Aspose.Pdf.Color.Yellow,
                Intent = FreeTextIntent.FreeTextCallout
            };

            // Set the Callout property with exactly three points (start, knee, end)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 550), // start point inside the annotation
                new Aspose.Pdf.Point(200, 620), // knee point (bend)
                new Aspose.Pdf.Point(250, 650)  // end point (target)
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}