using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CircleWithNote.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the first page
            Page page = doc.Pages[1];

            // Define the rectangle where the circle will be drawn
            Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a circle annotation
            CircleAnnotation circle = new CircleAnnotation(page, circleRect)
            {
                Color = Aspose.Pdf.Color.Red,
                Opacity = 0.5
            };
            page.Annotations.Add(circle); // attach circle to the page

            // Create a popup annotation that will be linked to the circle
            PopupAnnotation popup = new PopupAnnotation(page, circleRect)
            {
                // Title property is not available in older Aspose.Pdf versions; omit it.
                Contents = "Check figure",
                Parent = circle // link the popup to its parent annotation
            };
            page.Annotations.Add(popup); // attach popup to the page

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved: {outputPath}");
    }
}
