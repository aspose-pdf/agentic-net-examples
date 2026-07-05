using System;
using System.IO;
using System.Drawing;                     // needed for DefaultAppearance text color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;                    // for DefaultAppearance
using Aspose.Pdf.Drawing;                 // for Border (requires parent annotation)

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

        // Load the PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance for the text (requires System.Drawing.Color)
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation on the page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, da);
            freeText.Contents = "This is a free‑text annotation with a light gray background and rounded corners.";
            freeText.Color = Aspose.Pdf.Color.LightGray;

            // Set the border – Border requires the parent annotation in its constructor.
            // Rounded corners are not directly exposed via a CornerRadius property; the PDF viewer will render the annotation
            // with the default (square) corners. If rounded corners are required, they can be achieved via appearance streams
            // or by using a custom border effect, which is beyond the scope of this simple example.
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
