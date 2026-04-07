using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_screen.pdf"; // result PDF
        const string swfPath   = "animation.swf";      // external SWF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfPath}");
            return;
        }

        // Load the PDF document (using the lifecycle rule for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation that points to the external SWF file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, swfPath);

            // Disable user interaction by setting the annotation to read‑only
            screenAnn.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF (standard Save method writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdf}'.");
    }
}