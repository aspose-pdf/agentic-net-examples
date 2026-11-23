using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

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

            // Rectangle that bounds the line annotation
            Rectangle lineRect = new Rectangle(100, 500, 300, 500);

            // Create a line annotation with explicit start/end points
            LineAnnotation lineAnno = new LineAnnotation(page, lineRect,
                new Point(100, 500), new Point(300, 500))
            {
                Color = Color.FromRgb(0, 0, 1), // blue line
                StartingStyle = LineEnding.None,
                EndingStyle = LineEnding.None
            };
            // Set line thickness via Border.Width
            lineAnno.Border = new Border(lineAnno) { Width = 2 };

            // Rectangle for the popup annotation (where the popup will appear)
            Rectangle popupRect = new Rectangle(320, 500, 520, 600);
            PopupAnnotation popupAnno = new PopupAnnotation(page, popupRect)
            {
                Contents = "This is a popup attached to the line annotation.",
                Open = false
            };

            // Associate the popup with the line annotation
            lineAnno.Popup = popupAnno;

            // Add the line annotation (which now contains the popup) to the page
            page.Annotations.Add(lineAnno);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotation: '{outputPath}'.");
    }
}
