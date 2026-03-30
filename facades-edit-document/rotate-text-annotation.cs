using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text (sticky‑note) annotation
            TextAnnotation txtAnn = new TextAnnotation(page, rect);
            txtAnn.Title = "Note";
            txtAnn.Contents = "Rotated annotation";
            txtAnn.Color = Aspose.Pdf.Color.Yellow;

            // Rotate the annotation by 90 degrees clockwise
            txtAnn.Characteristics.Rotate = Aspose.Pdf.Rotation.on90;

            // Add the annotation to the page
            page.Annotations.Add(txtAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated annotation saved to '{outputPath}'.");
    }
}