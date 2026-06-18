using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_swfs.pdf"; // result PDF
        const string swfPath    = "animation.swf";      // external SWF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the screen annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation referencing the external SWF file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, swfPath);

            // Disable user interaction (make the annotation read‑only)
            screenAnn.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF (lifecycle rule: using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPath}");
    }
}