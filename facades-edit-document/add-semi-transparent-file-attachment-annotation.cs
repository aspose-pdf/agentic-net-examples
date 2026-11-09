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

        // Load the PDF document (loads only the needed parts into memory)
        Document doc = new Document(inputPath);

        // Verify that page 3 exists (pages are 1‑based)
        if (doc.Pages.Count < 3)
        {
            Console.Error.WriteLine("The document does not contain a page 3.");
            return;
        }

        // Define the annotation rectangle (llx, lly, urx, ury) in points
        var rect = new Rectangle(100, 500, 300, 550);

        // Retrieve the target page
        Page targetPage = doc.Pages[3];

        // Create a text annotation with semi‑transparent opacity
        var txtAnnot = new TextAnnotation(targetPage, rect)
        {
            Title    = "Note",
            Contents = "Semi‑transparent annotation",
            Open     = true,
            Icon     = TextIcon.Note,
            Opacity  = 0.6f // 60% opacity
        };

        // Add the annotation to the page's annotation collection
        targetPage.Annotations.Add(txtAnnot);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Annotation added to page 3 and saved as '{outputPath}'.");
    }
}
