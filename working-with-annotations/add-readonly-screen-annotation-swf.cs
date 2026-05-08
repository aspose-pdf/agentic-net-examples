using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string swfPath   = "animation.swf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the screen annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation referencing the external SWF file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, swfPath);

            // Disable user interaction by setting the annotation to read‑only
            screenAnn.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdf}'.");
    }
}