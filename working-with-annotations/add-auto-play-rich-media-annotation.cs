using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string mediaFile = "sample.mp4";         // video/audio to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Activate automatically when the page becomes visible
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible,

                // Optional: give the annotation a title/description
                Contents = "Embedded video that plays on page view"
            };

            // Embed the media content (video/audio) into the annotation
            using (FileStream mediaStream = File.OpenRead(mediaFile))
            {
                // The first parameter is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(mediaFile), mediaStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPdf}'.");
    }
}