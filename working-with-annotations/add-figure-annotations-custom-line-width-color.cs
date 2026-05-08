using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Example: add a red square annotation with line width 2 on pages 1 and 3
            int[] squarePages = { 1, 3 };
            foreach (int pageNum in squarePages)
            {
                if (pageNum > doc.Pages.Count) continue; // safety check

                Page page = doc.Pages[pageNum];

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create the square annotation
                SquareAnnotation square = new SquareAnnotation(page, rect);

                // Set border width (line width) via Border object
                square.Border = new Border(square) { Width = 2 };

                // Set annotation color
                square.Color = Aspose.Pdf.Color.Red;

                // Add to the page's annotation collection
                page.Annotations.Add(square);
            }

            // Example: add a blue circle annotation with line width 3 on page 2
            int circlePageNum = 2;
            if (circlePageNum <= doc.Pages.Count)
            {
                Page page = doc.Pages[circlePageNum];

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(300, 400, 400, 500);
                CircleAnnotation circle = new CircleAnnotation(page, rect);

                circle.Border = new Border(circle) { Width = 3 };
                circle.Color = Aspose.Pdf.Color.Blue;

                page.Annotations.Add(circle);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}