using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // Facades namespace as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAnnotation on the first page
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnnot = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is an orange text annotation.",
                Open     = true,
                // Set the annotation color to orange (RGB 255,165,0)
                Color    = Aspose.Pdf.Color.FromRgb(1.0, 0.6470588235, 0.0)
            };

            // Add the annotation to the page
            page.Annotations.Add(txtAnnot);

            // (Optional) Instantiate PdfContentEditor as the task mentions using Facades.
            // It is not required for adding the annotation, but demonstrates usage.
            PdfContentEditor editor = new PdfContentEditor();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}