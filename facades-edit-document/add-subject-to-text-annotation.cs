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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (llx, lly, urx, ury)
        var rect = new Rectangle(100, 500, 300, 550);

        // Get the first page (Aspose.Pdf pages are 1‑based)
        Page page = doc.Pages[1];

        // Create a text annotation on the retrieved page
        var annotation = new TextAnnotation(page, rect)
        {
            Title = "Note",
            Subject = "Important", // Subject for easier searching/categorization
            Contents = "This is a subject‑tagged annotation.",
            Icon = TextIcon.Note,
            Open = true
        };

        // Add the annotation to the page's Annotations collection
        page.Annotations.Add(annotation);

        // Save the modified document
        doc.Save(outputPath);

        Console.WriteLine($"Annotation added with subject. Saved to '{outputPath}'.");
    }
}
