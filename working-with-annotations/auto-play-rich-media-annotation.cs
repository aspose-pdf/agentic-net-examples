using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_richmedia.pdf"; // result PDF
        const string videoPath  = "sample.mp4";         // video to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page, 1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (left, bottom, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Create the RichMediaAnnotation first
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Set properties after the object is instantiated (Border needs the parent annotation)
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.PageVisible;
            richMedia.Color = Aspose.Pdf.Color.LightGray;
            richMedia.Border = new Border(richMedia) { Width = 1 };

            // Embed the video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                // The first argument is the name of the stream inside the PDF
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPdf}'.");
    }
}
