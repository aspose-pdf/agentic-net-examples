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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (page numbers are 1‑based)
            Page page = doc.Pages[1];

            // Rectangle coordinates (lower‑left x/y, upper‑right x/y)
            double llx = 100, lly = 500, urx = 300, ury = 550;
            var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // ------------------------------------------------------------
            // 1. Draw a rectangle – use a SquareAnnotation.
            // ------------------------------------------------------------
            var square = new SquareAnnotation(page, rect)
            {
                Color = Color.Black // border colour
                // FillColor property is not available in this version of Aspose.PDF, so it is omitted.
            };
            // Set border thickness (default is 1, but shown explicitly)
            square.Border = new Border(square) { Width = 1 };
            page.Annotations.Add(square);

            // ------------------------------------------------------------
            // 2. Add a JavaScript link that opens a mailto URL when clicked.
            // ------------------------------------------------------------
            var jsAction = new JavascriptAction("app.launchURL('mailto:someone@example.com');");
            var link = new LinkAnnotation(page, rect)
            {
                Action = jsAction
            };
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
