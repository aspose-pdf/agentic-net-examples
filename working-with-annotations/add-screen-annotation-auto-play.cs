using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace AddScreenAnnotationAutoPlay
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF document
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and add a ScreenAnnotation
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

                // Create a ScreenAnnotation that points to an external video URL
                ScreenAnnotation screen = new ScreenAnnotation(page, rect, "https://example.com/video.mp4");
                screen.Contents = "External video";
                screen.Color = Aspose.Pdf.Color.Yellow;

                // Add the annotation to the page
                page.Annotations.Add(screen);

                // Add a document‑level JavaScript action that starts playback automatically when the document is opened
                // The script retrieves the first annotation on the first page and calls its play() method.
                JavascriptAction openJs = new JavascriptAction("var ann = this.getAnnots()[0]; if (ann) ann.play();");
                doc.OpenAction = openJs;

                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
