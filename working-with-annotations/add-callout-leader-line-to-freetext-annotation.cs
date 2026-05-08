using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_callout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",               // font name
                12,                        // font size
                System.Drawing.Color.Black // text color
            );

            // Instantiate the FreeTextAnnotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, annotRect, appearance)
            {
                Contents = "This is a callout annotation.",
                Color    = Aspose.Pdf.Color.Yellow, // border color
                Intent   = FreeTextIntent.FreeTextCallout
            };

            // Set the callout leader line (exactly three points)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 540), // point inside the annotation rectangle
                new Aspose.Pdf.Point(200, 600), // knee point (bend)
                new Aspose.Pdf.Point(250, 650)  // target point where the line points to
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with callout annotation to '{outputPath}'.");
    }
}