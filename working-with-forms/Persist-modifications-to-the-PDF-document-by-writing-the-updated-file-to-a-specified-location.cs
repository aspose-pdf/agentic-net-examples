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

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1]; // Aspose.Pdf uses 1‑based page indexing

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation and set its properties
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Sample annotation added programmatically.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Attach the annotation to the page
            page.Annotations.Add(txtAnn);

            // Persist the modified document to the specified location
            // (lifecycle rule: use Document.Save(string))
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}