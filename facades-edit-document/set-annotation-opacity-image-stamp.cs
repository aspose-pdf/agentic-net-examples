using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required namespace

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

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Use the first page for the annotation (adjust as needed)
                Page page = doc.Pages[1];

                // Define annotation rectangle (llx, lly, urx, ury) in points
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation
                TextAnnotation annotation = new TextAnnotation(page, rect);
                annotation.Title = "Note";
                annotation.Contents = "Subtle annotation with opacity.";
                annotation.Open = true;
                annotation.Icon = TextIcon.Note;

                // Set visual property: 75% opacity.
                // BlendMode is not supported on TextAnnotation in the current Aspose.PDF version, so it is omitted.
                annotation.Opacity = 0.75f;

                // Add the annotation to the page
                page.Annotations.Add(annotation);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
