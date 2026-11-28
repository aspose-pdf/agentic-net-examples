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

        // Load the PDF document.
        var doc = new Document(inputPath);

        // Define rectangle area (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
        var rect = new Rectangle(100, 500, 300, 550);

        // ---------------------------------------------------------------------
        // 1. Draw a visible rectangle on page 1.
        // ---------------------------------------------------------------------
        var page = doc.Pages[1];
        var square = new SquareAnnotation(page, rect)
        {
            Color = Color.Black // border colour
        };
        // Border must be created with the owning annotation as the constructor argument.
        square.Border = new Border(square) { Width = 2 };
        page.Annotations.Add(square);

        // ---------------------------------------------------------------------
        // 2. Add a JavaScript action that shows an alert when the rectangle is clicked.
        // ---------------------------------------------------------------------
        string jsCode = "app.alert('Rectangle clicked!');";
        var link = new LinkAnnotation(page, rect)
        {
            Action = new JavascriptAction(jsCode)
        };
        page.Annotations.Add(link);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with rectangle and JavaScript link to '{outputPath}'.");
    }
}
