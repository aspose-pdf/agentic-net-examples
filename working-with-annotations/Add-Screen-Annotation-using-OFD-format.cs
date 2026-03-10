using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input OFD file and media file paths
        const string inputOfdPath = "input.ofd";
        const string mediaFilePath = "sample.mp4";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input OFD not found: {inputOfdPath}");
            return;
        }

        if (!File.Exists(mediaFilePath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFilePath}");
            return;
        }

        // Load OFD using the official load rule (OFD is input‑only)
        using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 50);

            // Create the ScreenAnnotation with the media file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaFilePath)
            {
                // Optional: set a title and border color
                Title = "Sample Video",
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnn);

            // Save the result as PDF (OFD cannot be saved)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Screen annotation added and saved to '{outputPdfPath}'.");
    }
}