using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Load the PDF with PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (fully‑qualified type)
            Aspose.Pdf.Rectangle annRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Get the first page from the underlying Document
            Page page = editor.Document.Pages[1];

            // Create a line annotation – modern API requires start/end points in the constructor
            LineAnnotation line = new LineAnnotation(
                page,
                annRect,
                new Aspose.Pdf.Point(100, 500),   // start point
                new Aspose.Pdf.Point(300, 500)    // end point
            );

            // Set line colour and width (optional, but demonstrates full usage)
            line.Color = Aspose.Pdf.Color.Blue;
            line.Border = new Border(line) { Width = 2 };

            // Set line caps (both start and end) to round (circle)
            line.StartingStyle = LineEnding.Circle; // round cap at the start
            line.EndingStyle   = LineEnding.Circle; // round cap at the end

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
