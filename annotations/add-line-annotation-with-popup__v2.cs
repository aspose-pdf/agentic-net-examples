using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_line_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            Page page = doc.Pages[1];

            // Rectangle that defines the annotation's bounding box
            Rectangle rect = new Rectangle(100, 500, 300, 500);

            // Create the line annotation with start and end points (constructor signature required by Aspose.Pdf)
            LineAnnotation line = new LineAnnotation(page, rect,
                new Point(100, 500), new Point(300, 500))
            {
                Color = Color.Blue
            };
            // Set line thickness via Border
            line.Border = new Border(line) { Width = 2 };

            // Popup annotation – Title property is not available in this version, so it is omitted
            Rectangle popupRect = new Rectangle(310, 500, 410, 550);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a line annotation with a popup."
            };

            // Associate the popup with the line annotation
            line.Popup = popup;

            // Add the line annotation (which now contains the popup) to the page
            page.Annotations.Add(line);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with line annotation to '{outputPath}'.");
    }
}
