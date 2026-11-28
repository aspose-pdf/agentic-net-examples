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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Ensure the document has at least two pages.
        const int pageNumber = 2; // 1‑based index
        if (pdf.Pages.Count < pageNumber)
        {
            Console.Error.WriteLine($"The PDF has only {pdf.Pages.Count} page(s). Cannot access page {pageNumber}.");
            return;
        }
        Page page = pdf.Pages[pageNumber];

        // Define the rectangle area (lower‑left X/Y, upper‑right X/Y).
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 600; // upper‑right Y
        var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // 1. Draw a black rectangle on page 2 using SquareAnnotation.
        var rectangleAnnotation = new SquareAnnotation(page, rect)
        {
            Color = Aspose.Pdf.Color.Black,
            // Set a visible border (default width is 1).
            Border = new Border(null) { Width = 1 }
        };
        // The Border constructor expects the parent annotation; assign after creation.
        rectangleAnnotation.Border = new Border(rectangleAnnotation) { Width = 1 };
        page.Annotations.Add(rectangleAnnotation);

        // 2. Add a JavaScript link over the same rectangle that submits form data.
        string javaScript = "this.submitForm({cURL:'https://example.com/submit'});";
        var jsAction = new JavascriptAction(javaScript);
        var linkAnnotation = new LinkAnnotation(page, rect)
        {
            Action = jsAction
        };
        page.Annotations.Add(linkAnnotation);

        // Save the modified PDF.
        pdf.Save(outputPath);
        Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
    }
}
