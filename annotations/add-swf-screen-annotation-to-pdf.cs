using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string swfFilePath = "animation.swf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(swfFilePath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfFilePath}");
            return;
        }

        using (Document doc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based.
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear.
            // (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // ScreenAnnotation requires the media file path in its constructor.
            ScreenAnnotation screenAnno = new ScreenAnnotation(page, rect, swfFilePath)
            {
                Name = "SampleSWF",
                Title = "SWF Animation",
                // Border colour is set via the annotation's Color property.
                Color = Color.Black
            };

            // Border must be created with the parent annotation; it has no Color property.
            screenAnno.Border = new Border(screenAnno) { Width = 1 };

            // Add the annotation to the page.
            page.Annotations.Add(screenAnno);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Screen annotation added and PDF saved to '{outputPdfPath}'.");
    }
}
