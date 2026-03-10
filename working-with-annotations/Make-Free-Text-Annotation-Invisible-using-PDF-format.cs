using System;
using System.Drawing; // needed for System.Drawing.Color in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

// Alias to disambiguate the Color type between System.Drawing and Aspose.Pdf
using SystemColor = System.Drawing.Color;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "invisible_freetext.pdf";

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the DefaultAppearance – use the aliased SystemColor to avoid ambiguity
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, SystemColor.Black);

            // Create the FreeTextAnnotation and set the Invisible flag (not displayed on screen or print)
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This annotation is invisible",
                Flags = AnnotationFlags.Invisible // use Invisible flag as requested
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation made invisible and saved to '{outputPath}'.");
    }
}
