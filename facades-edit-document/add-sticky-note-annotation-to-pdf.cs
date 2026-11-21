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

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        // This creates a rectangle 200 points wide and 100 points high positioned at (100,500).
        var rect = new Rectangle(100, 500, 300, 600);

        // Create a sticky‑note (text) annotation.
        var textAnnotation = new TextAnnotation(doc.Pages[1], rect)
        {
            Title = "User Comment",
            Contents = "This is a sticky note added via Aspose.Pdf.",
            Icon = TextIcon.Note,   // optional: choose the note icon
            Open = true             // optional: open the note when the PDF is opened
        };

        // Add the annotation to the first page.
        doc.Pages[1].Annotations.Add(textAnnotation);

        // Save the annotated PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Sticky note annotation added. Output saved to '{outputPath}'.");
    }
}
