using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based index)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Build the default appearance (font name, size, color)
            // NOTE: DefaultAppearance expects System.Drawing.Color, not Aspose.Pdf.Color
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the required constructor
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Text displayed inside the annotation
                Contents = "Callout annotation",
                // Background colour of the annotation (use Aspose.Pdf.Color explicitly)
                Color = Aspose.Pdf.Color.LightGray,
                // Set the intent to a callout
                Intent = FreeTextIntent.FreeTextCallout,
                // Define the callout points (target, knee, box edge)
                Callout = new[]
                {
                    new Aspose.Pdf.Point(150, 520),
                    new Aspose.Pdf.Point(200, 540),
                    new Aspose.Pdf.Point(250, 560)
                },
                // Arrow style at the end of the leader line
                EndingStyle = LineEnding.ClosedArrow
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FreeTextCallout annotation to '{outputPath}'.");
    }
}
