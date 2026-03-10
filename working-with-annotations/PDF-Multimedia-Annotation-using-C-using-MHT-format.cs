using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string mhtFile          = Path.Combine(dataDir, "sample.mht");
        string videoFile        = Path.Combine(dataDir, "sample.mp4");
        string outputPdf        = Path.Combine(dataDir, "MHT_With_Video.pdf");

        // Verify input files exist
        if (!File.Exists(mhtFile))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtFile}");
            return;
        }
        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtFile, loadOptions))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = pdfDoc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichMediaAnnotation (video/audio) on the selected page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect)
            {
                // Set the type to Video (alternatively Audio)
                Type = RichMediaAnnotation.ContentType.Video,
                // Optional visual properties
                Color = Aspose.Pdf.Color.LightGray,
                // Set a title/description
                Contents = "Embedded video annotation"
            };

            // Attach the video content to the annotation
            using (FileStream videoStream = File.OpenRead(videoFile))
            {
                // MIME type for MP4 video
                richMedia.SetContent("video/mp4", videoStream);
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with multimedia annotation saved to '{outputPdf}'.");
    }
}
