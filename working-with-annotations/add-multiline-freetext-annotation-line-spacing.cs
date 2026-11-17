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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Create a DefaultAppearance (font, size, color) required by FreeTextAnnotation
            var defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a free‑text annotation with the rectangle and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, defaultAppearance);

            // Multiline content – use HTML‑style rich text to control line spacing (1.5 = 150%)
            freeText.RichText = "<body style='line-height:150%'>First line<br/>Second line<br/>Third line</body>";

            // Optional: set the annotation border color (or any other visual properties)
            freeText.Color = Aspose.Pdf.Color.LightGray; // background color of the annotation

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
