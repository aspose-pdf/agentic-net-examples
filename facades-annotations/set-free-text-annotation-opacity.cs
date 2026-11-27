using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used by DefaultAppearance
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

        // Load the source PDF
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // Define the annotation rectangle (Aspose.Pdf.Rectangle) and appearance
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        // DefaultAppearance expects System.Drawing.Color, so use the fully‑qualified type
        var defaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

        // Create the FreeText annotation with 50 % opacity
        FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, defaultAppearance)
        {
            Contents = "Sample free text",
            // Background colour of the annotation (Aspose.Pdf.Color)
            Color = Aspose.Pdf.Color.LightGray,
            // Opacity (0‑1 range)
            Opacity = 0.5f
        };

        // Optional thin border around the annotation
        freeText.Border = new Border(freeText) { Width = 1 };

        // Add the annotation to the page and save
        page.Annotations.Add(freeText);
        doc.Save(outputPath);

        Console.WriteLine($"FreeText annotation with 50 % opacity saved to '{outputPath}'.");
    }
}
