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
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a sticky note (text annotation)
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a sticky annotation.",
                Open     = true,
                Icon     = TextIcon.Comment   // visual icon for the sticky note
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Retrieve the annotation we just added (first annotation, collection is 1‑based)
            Annotation retrieved = page.Annotations[1];
            Console.WriteLine($"Retrieved annotation contents: {retrieved.Contents}");

            // Delete the annotation (by its index)
            page.Annotations.Delete(1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}