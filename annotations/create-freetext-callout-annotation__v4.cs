using System;
using System.IO;
using System.Drawing; // needed for DefaultAppearance color
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
            // First page
            Page page = doc.Pages[1];

            // Annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Default appearance required by FreeTextAnnotation constructor
            // DefaultAppearance expects a System.Drawing.Color, so we keep the System.Drawing namespace.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the required appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Callout annotation",
                Intent = FreeTextIntent.FreeTextCallout, // use enum, not string
                EndingStyle = LineEnding.OpenArrow,      // correct enum for arrow style
                Color = Aspose.Pdf.Color.LightGray      // optional background colour (fully qualified)
            };

            // Add annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation with Intent='FreeTextCallout' and EndingStyle='OpenArrow' saved to '{outputPath}'.");
    }
}
