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
        const string mediaPath = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a RichMediaAnnotation on the page
            RichMediaAnnotation rich = new RichMediaAnnotation(page, rect)
            {
                // Specify that the content is a video
                Type = RichMediaAnnotation.ContentType.Video,
                // Activate automatically when the page becomes visible
                ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible
            };

            // Embed the video file into the annotation
            using (FileStream mediaStream = File.OpenRead(mediaPath))
            {
                rich.SetContent(Path.GetFileName(mediaPath), mediaStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(rich);

            // Save the modified PDF (lifecycle rule: save within using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}