using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tooltip_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (lower‑left x,y, upper‑right x,y)
        var rect = new Rectangle(100, 500, 200, 550);

        // Create a text annotation (tooltip)
        var textAnnotation = new TextAnnotation(doc.Pages[1], rect)
        {
            Title = "Info",
            Contents = "Additional information shown when the mouse hovers over this area.",
            Icon = TextIcon.Note,
            Open = false // not opened by default; appears as tooltip on hover
        };

        // Add the annotation to the first page
        doc.Pages[1].Annotations.Add(textAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Tooltip annotation added and saved to '{outputPath}'.");
    }
}