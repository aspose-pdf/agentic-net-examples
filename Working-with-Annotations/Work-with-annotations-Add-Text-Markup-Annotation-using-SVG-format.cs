using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Rectangle if needed, but we will fully qualify

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "annotated_output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, add a text markup annotation, and save as SVG
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page to annotate (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a squiggly (jagged underline) text markup annotation
            SquigglyAnnotation squiggly = new SquigglyAnnotation(page, rect)
            {
                // Set the annotation appearance
                Color = Aspose.Pdf.Color.Red,          // cross‑platform color
                Opacity = 0.5,
                Contents = "Highlighted text",
                Title = "Reviewer"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(squiggly);

            // Save the modified document as SVG using explicit SvgSaveOptions
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            doc.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"Annotated SVG saved to '{outputSvgPath}'.");
    }
}