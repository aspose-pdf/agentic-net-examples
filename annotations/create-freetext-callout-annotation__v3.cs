using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        using (Document doc = new Document(inputPath))
        {
            // first page
            Page page = doc.Pages[1];

            // annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // FreeTextAnnotation requires a DefaultAppearance object (System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free text annotation",
                Title    = "Note",
                // background colour of the annotation (Aspose.Pdf.Color)
                Color    = Aspose.Pdf.Color.Yellow
            };

            // optional border with rounded corners
            freeText.Border = new Border(freeText) { Width = 1, HCornerRadius = 4, VCornerRadius = 4 };

            // callout points (target, knee, box edge)
            freeText.Callout = new[]
            {
                new Aspose.Pdf.Point(120, 520), // start (near annotation)
                new Aspose.Pdf.Point(150, 480), // knee (optional)
                new Aspose.Pdf.Point(200, 440)  // end (target)
            };

            // indicate that this is a callout and add an arrow tip
            freeText.Intent = FreeTextIntent.FreeTextCallout;
            freeText.EndingStyle = LineEnding.OpenArrow;

            page.Annotations.Add(freeText);

            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation with callout saved to '{outputPath}'.");
    }
}
