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

        // Load the PDF, create a text annotation and set visual properties.
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the annotation (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the markup annotation with opacity. BlendMode is not supported in the current Aspose.Pdf version.
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "Sample annotation",
                Open     = true,
                Icon     = TextIcon.Note,
                Opacity  = 0.75f // 75% opacity
                // BlendMode = BlendMode.Multiply // <-- not available in this version
            };

            // Add the annotation to the first page.
            doc.Pages[1].Annotations.Add(annotation);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with opacity saved to '{outputPath}'.");
    }
}
