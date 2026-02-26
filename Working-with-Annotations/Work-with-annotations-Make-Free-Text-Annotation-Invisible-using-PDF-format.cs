using System;
using System.IO;
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

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page (1‑based indexing)
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Define rectangle for the free‑text annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a free‑text annotation on the first page
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, new DefaultAppearance());

            freeText.Contents = "This annotation will be hidden.";
            // Optional: set a transparent color (cross‑platform Aspose.Pdf.Color)
            freeText.Color = Aspose.Pdf.Color.Transparent;

            // Create a HideAction that hides the annotation (isHidden = true)
            HideAction hide = new HideAction(freeText, true);
            // Add the hide action to the annotation's action collection
            freeText.Actions.Add(hide);

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Invisible FreeTextAnnotation saved to '{outputPath}'.");
    }
}