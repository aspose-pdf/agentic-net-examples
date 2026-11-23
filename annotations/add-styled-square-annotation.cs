using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SquareStyled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

            // Create the square annotation with colour and interior colour
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Black,          // border colour
                InteriorColor = Aspose.Pdf.Color.Yellow   // fill colour
            };

            // Border must be created with the parent annotation and then configured
            square.Border = new Border(square)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved annotated PDF to '{outputPath}'.");
    }
}
