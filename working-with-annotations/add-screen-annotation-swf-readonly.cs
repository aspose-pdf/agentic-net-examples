using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_screen.pdf";
        const string swfPath = "animation.swf";

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page to host the annotation (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation that references the external SWF file
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, swfPath);

            // Disable user interaction: make the annotation read‑only and locked
            screen.Flags = AnnotationFlags.ReadOnly | AnnotationFlags.Locked;

            // Optional metadata
            screen.Title = "Embedded SWF";
            screen.Contents = "Flash animation";

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPath}");
    }
}