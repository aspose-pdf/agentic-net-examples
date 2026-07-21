using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text annotation on the first page
            Page page = doc.Pages[1];
            Rectangle rect = new Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Contents = "Sample annotation",
                Color    = Color.Yellow,
                Opacity  = 0.75 // Set opacity to 75%
                // Note: BlendMode is not supported in the current Aspose.PDF version.
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with opacity 0.75. Saved to '{outputPath}'.");
    }
}
