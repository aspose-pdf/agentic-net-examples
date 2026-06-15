using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

namespace ExtractHighlightAnnotationImage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with some text and a highlight annotation.
            using (Document sampleDoc = new Document())
            {
                Page samplePage = sampleDoc.Pages.Add();
                TextFragment text = new TextFragment("This is a sample text to be highlighted.");
                samplePage.Paragraphs.Add(text);

                // Define a rectangle that roughly covers the text.
                Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 500, 400, 480);
                HighlightAnnotation highlight = new HighlightAnnotation(samplePage, highlightRect);
                highlight.Color = Color.Yellow;
                samplePage.Annotations.Add(highlight);

                sampleDoc.Save("sample.pdf");
            }

            // Step 2: Open the PDF, locate the highlight annotation and render the page to an image.
            using (Document srcDoc = new Document("sample.pdf"))
            {
                Page srcPage = srcDoc.Pages[1]; // 1‑based page indexing
                HighlightAnnotation highlight = srcPage.Annotations[1] as HighlightAnnotation;
                if (highlight == null)
                {
                    Console.WriteLine("No highlight annotation found.");
                    return;
                }

                // Render the first page to a PNG image stored in a memory stream.
                using (MemoryStream pageImageStream = new MemoryStream())
                {
                    // Use PngDevice (recommended over ImageSaveOptions).
                    PngDevice pngDevice = new PngDevice(new Resolution(300));
                    pngDevice.Process(srcPage, pageImageStream);
                    pageImageStream.Position = 0;

                    // Step 3: Create a new PDF report and embed the rendered image.
                    using (Document reportDoc = new Document())
                    {
                        Page reportPage = reportDoc.Pages.Add();
                        Aspose.Pdf.Image reportImage = new Aspose.Pdf.Image();
                        reportImage.ImageStream = pageImageStream;
                        // Set desired size for the image on the report page.
                        reportImage.FixWidth = 400;
                        reportImage.FixHeight = 500;
                        reportPage.Paragraphs.Add(reportImage);

                        reportDoc.Save("report.pdf");
                    }
                }
            }
        }
    }
}
