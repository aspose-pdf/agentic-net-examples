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
        const string swfPath = "animation.swf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x,y and upper‑right x,y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);

            // Create a ScreenAnnotation that references an external SWF file.
            // The constructor requires the media file path as the third argument.
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, swfPath);
            // Disable user interaction (prevent editing) by marking the annotation as read‑only.
            screen.Flags = AnnotationFlags.ReadOnly;

            // Add the annotation to the page
            page.Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPath}'.");
    }
}
