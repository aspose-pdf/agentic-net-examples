using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document does not contain any pages.");
                return;
            }

            // Get the first page (1‑based indexing).
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define the start and end points of the separator line.
            Aspose.Pdf.Point startPoint = new Aspose.Pdf.Point(50, 500);
            Aspose.Pdf.Point endPoint   = new Aspose.Pdf.Point(550, 500);

            // Define a rectangle that encloses the line.
            // Rectangle(llx, lly, urx, ury)
            Aspose.Pdf.Rectangle lineRect = new Aspose.Pdf.Rectangle(50, 495, 550, 505);

            // Create the line annotation.
            Aspose.Pdf.Annotations.LineAnnotation lineAnnotation =
                new Aspose.Pdf.Annotations.LineAnnotation(page, lineRect, startPoint, endPoint);

            // Set visual properties (color, optional border width, etc.).
            lineAnnotation.Color = Aspose.Pdf.Color.Black;
            lineAnnotation.Border = new Border(lineAnnotation) { Width = 1 };

            // Add the annotation to the page.
            page.Annotations.Add(lineAnnotation);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator line added. Saved to '{outputPath}'.");
    }
}