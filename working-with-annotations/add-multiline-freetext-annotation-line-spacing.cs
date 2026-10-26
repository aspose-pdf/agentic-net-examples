using System;
using System.IO;
using System.Drawing; // Required for System.Drawing.Color in DefaultAppearance
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Create the required DefaultAppearance (font name, size, color)
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.DarkBlue);

            // Construct the FreeTextAnnotation with page, rectangle and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Multiline content using newline characters (fallback plain text)
                Contents = "First line of text.\nSecond line of text.\nThird line of text."
            };

            // Set background (fill) color – use the Annotation.Color property
            freeText.Color = Aspose.Pdf.Color.LightGray;

            // Set border – Border constructor takes only the owning annotation
            freeText.Border = new Border(freeText) { Width = 1 }; // Width expects an int

            // Configure rich‑text appearance to control line spacing (1.5)
            // Using simple HTML with CSS line‑height
            freeText.RichText = "<body style='font-family:Arial; font-size:12pt; color:#00008B; line-height:1.5'>" +
                               "First line of text.<br/>" +
                               "Second line of text.<br/>" +
                               "Third line of text.</body>";

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
