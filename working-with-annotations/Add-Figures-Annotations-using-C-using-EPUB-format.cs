using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Ensure there is at least one page
                if (doc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The document has no pages.");
                    return;
                }

                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define a rectangle for the square annotation (left, bottom, right, top)
                Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                // Create a square (figure) annotation
                SquareAnnotation square = new SquareAnnotation(page, squareRect)
                {
                    Color = Aspose.Pdf.Color.Yellow,
                    Contents = "Square Figure Annotation"
                };
                // Add the square annotation to the page
                page.Annotations.Add(square);

                // Define a rectangle for the circle annotation
                Aspose.Pdf.Rectangle circleRect = new Aspose.Pdf.Rectangle(300, 500, 500, 700);
                // Create a circle (figure) annotation
                CircleAnnotation circle = new CircleAnnotation(page, circleRect)
                {
                    Color = Aspose.Pdf.Color.LightBlue,
                    Contents = "Circle Figure Annotation"
                };
                // Add the circle annotation to the page
                page.Annotations.Add(circle);

                // Prepare EPUB save options (optional title)
                Aspose.Pdf.EpubSaveOptions epubOptions = new Aspose.Pdf.EpubSaveOptions
                {
                    Title = "PDF with Figure Annotations"
                };

                // Save the document as EPUB
                doc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"EPUB file created successfully: {outputEpub}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
