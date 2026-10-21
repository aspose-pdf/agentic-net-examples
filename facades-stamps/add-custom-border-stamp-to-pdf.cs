using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the stamp will be placed
                // (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                // Example positions the stamp near the top‑left corner
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    llx: 50,
                    lly: page.PageInfo.Height - 100,
                    urx: 300,
                    ury: page.PageInfo.Height - 50);

                // Create a TextAnnotation which will act as the visual stamp
                TextAnnotation ann = new TextAnnotation(page, rect)
                {
                    Title    = "Important",
                    Contents = "Review this section",
                    // Background color of the annotation (highlight color)
                    Color = Aspose.Pdf.Color.Yellow
                };

                // Set border thickness; border color is taken from the annotation's Color
                ann.Border = new Border(ann) { Width = 2 };

                // Attach the annotation to the page
                page.Annotations.Add(ann);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}