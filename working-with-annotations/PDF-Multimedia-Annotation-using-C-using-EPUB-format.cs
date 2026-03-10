using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF (will be created if missing)
        const string outputEpub = "output.epub";     // resulting EPUB file
        const string mediaFile = "sample.mp4";       // video or audio file to embed

        // Ensure a source PDF exists – create a simple one if not
        if (!File.Exists(pdfPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("PDF with multimedia annotation"));
                doc.Save(pdfPath);
            }
        }

        // Verify the media file is present
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the PDF, add a multimedia annotation, and save as EPUB
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Use the first page for the annotation
            Page page = pdfDoc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a ScreenAnnotation that references the media file
            ScreenAnnotation mediaAnn = new ScreenAnnotation(page, rect, mediaFile);
            // Optional visual styling
            mediaAnn.Color = Aspose.Pdf.Color.LightGray;
            mediaAnn.Border = new Border(mediaAnn) { Width = 1 };

            // Attach the annotation to the page
            page.Annotations.Add(mediaAnn);

            // Configure EPUB save options (use flow layout for better reflow on e‑readers)
            EpubSaveOptions epubOpts = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow,
                Title = "PDF with Multimedia"
            };

            // Save the modified document as EPUB
            pdfDoc.Save(outputEpub, epubOpts);
        }

        Console.WriteLine($"EPUB file created: {outputEpub}");
    }
}
