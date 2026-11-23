using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation with title and contents
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note Title",
                Contents = "This is the annotation text.",
                Open = true,                     // Open the annotation by default
                Icon = TextIcon.Note             // Use the standard note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}