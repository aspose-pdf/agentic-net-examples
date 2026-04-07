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
        const string mediaFile = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the source PDF (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation on the page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Play automatically when the page becomes visible
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible,
                // Optional description shown in the annotation panel
                Contents = "Embedded video"
            };

            // Embed the media file (e.g., MP4) into the annotation
            using (FileStream mediaStream = File.OpenRead(mediaFile))
            {
                // The first argument is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(mediaFile), mediaStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}