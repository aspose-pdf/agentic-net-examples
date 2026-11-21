using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoFile = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            while (doc.Pages.Count < 3)
                doc.Pages.Add();

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a ScreenAnnotation on page 3 with the rectangle and the media file
            ScreenAnnotation screen = new ScreenAnnotation(doc.Pages[3], rect, videoFile)
            {
                // Optional description shown in the annotation UI
                Contents = "Sample Video"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[3].Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation with video added to page 3. Saved as '{outputPdf}'.");
    }
}
