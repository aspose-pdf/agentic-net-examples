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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAnnotation (sticky note) on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation tooltip = new TextAnnotation(doc.Pages[1], rect);

            // Set the tooltip text (shown when the mouse hovers over the annotation)
            tooltip.Title = "Info";               // optional title shown in the popup window title bar
            tooltip.Contents = "Additional information displayed as a tooltip when hovered.";
            tooltip.Icon = TextIcon.Comment;       // choose an icon style
            tooltip.Open = false;                  // keep the popup closed; tooltip appears on hover

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(tooltip);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip annotation added and saved to '{outputPath}'.");
    }
}
