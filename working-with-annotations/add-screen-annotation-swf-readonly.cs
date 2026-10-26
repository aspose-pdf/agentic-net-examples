using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_screen_annotation.pdf";
        const string swfPath = "movie.swf"; // external SWF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points; lower‑left to upper‑right)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation that references the external SWF file
            // The constructor requires the page, rectangle, and the media file path.
            ScreenAnnotation screenAnno = new ScreenAnnotation(page, rect, swfPath);

            // Disable user interaction (make the annotation read‑only)
            screenAnno.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(screenAnno);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPdf}");
    }
}
