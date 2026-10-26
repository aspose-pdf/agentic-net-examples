using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "popup_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the target page (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation icon will be placed
            // (lower‑left X,Y, upper‑right X,Y) – coordinates are in points
            var rect = new Rectangle(100, 700, 120, 720);

            // Create a popup annotation. The PopupAnnotation class belongs to
            // Aspose.Pdf.Annotations and is added directly to the page's Annotations collection.
            // NOTE: PopupAnnotation does NOT expose a Title property (it is defined on MarkupAnnotation).
            // Therefore we only set the Contents property here.
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                Contents = "This is a hover note displayed in the popup."
                // Title = "Note" // <-- removed: not available on PopupAnnotation
            };

            // Add the annotation to the page
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}
