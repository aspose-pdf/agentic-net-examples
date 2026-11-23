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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the line annotation
            Rectangle lineRect = new Rectangle(100, 500, 300, 500);

            // Create the line annotation – use the constructor that includes start/end points
            LineAnnotation lineAnn = new LineAnnotation(page, lineRect,
                new Point(100, 500), new Point(300, 500))
            {
                Color = Color.Blue,
                Contents = "Sample line annotation",
                StartingStyle = LineEnding.None,
                EndingStyle = LineEnding.ClosedArrow
            };

            // Create a popup annotation that will be shown when the line is selected
            Rectangle popupRect = new Rectangle(320, 520, 520, 420);
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                Contents = "Details about the line annotation"
                // Title property is not available in older versions – omitted intentionally
            };

            // Associate the popup with the line annotation
            lineAnn.Popup = popup;

            // Add both annotations to the page
            page.Annotations.Add(lineAnn);
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
