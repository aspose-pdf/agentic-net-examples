using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            // Pages are 1‑based indexed
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create and configure the text annotation
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This PDF has been modified.",
                Open = true,
                Icon = TextIcon.Note
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified document to a new file name, preserving the original
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}