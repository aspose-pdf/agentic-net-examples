using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle using fully‑qualified types
            Aspose.Pdf.Rectangle annRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation and set its visual properties
            TextAnnotation ann = new TextAnnotation(page, annRect)
            {
                Title    = "Note",
                Contents = "This annotation is locked and cannot be edited.",
                Color    = Aspose.Pdf.Color.Yellow,
                Icon     = TextIcon.Note,
                Open     = true
            };

            // Lock the annotation to prevent further modifications by end users
            ann.Flags = AnnotationFlags.Locked;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(ann);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with a locked annotation at '{outputPath}'.");
    }
}