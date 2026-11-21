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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Add a text annotation directly to the page (PdfAnnotationEditor.AddTextAnnotation does not exist)
            Page page = doc.Pages[1];
            var textAnnotation = new TextAnnotation(page, rect)
            {
                Contents = "Annotation with UTC creation date",
                // Use the existing Modified property to store the timestamp (CreationDate is not available)
                Modified = DateTime.UtcNow
            };
            page.Annotations.Add(textAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with UTC timestamp. Saved to '{outputPath}'.");
    }
}
