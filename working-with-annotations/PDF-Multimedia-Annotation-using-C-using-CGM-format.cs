using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";      // source CGM file
        const string mediaPath = "sample.mp4";     // video or audio to embed
        const string outputPdf = "output.pdf";     // resulting PDF

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load CGM and convert it to a PDF document
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Create a rectangle where the multimedia annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Specify the type of media (Video or Audio)
            richMedia.Type = RichMediaAnnotation.ContentType.Video;

            // Embed the media file content
            using (FileStream mediaStream = File.OpenRead(mediaPath))
            {
                // The first parameter is an arbitrary name for the content stream
                richMedia.SetContent("media", mediaStream);
            }

            // Optional: set visible contents for the annotation (Title property is not supported for RichMediaAnnotation)
            richMedia.Contents = "Click to play the embedded video.";

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Use PdfAnnotationEditor (from Aspose.Pdf.Facades) to save the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.Save(outputPdf);
            editor.Close();

            Console.WriteLine($"PDF with multimedia annotation saved to '{outputPdf}'.");
        }
    }
}
