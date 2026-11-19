using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "videos.xml";    // XML describing videos
        const string outputPdf = "output_with_videos.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load XML that contains video references.
        // Expected format (example):
        // <Videos>
        //   <Video page="1" x="100" y="500" width="200" height="150">video1.mp4</Video>
        //   <Video page="2" x="50"  y="400" width="300" height="200">video2.mov</Video>
        // </Videos>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            foreach (XElement videoElem in xmlDoc.Root.Elements("Video"))
            {
                // Parse attributes; fall back to defaults if missing.
                int pageNumber = (int?)videoElem.Attribute("page") ?? 1;
                double llx = (double?)videoElem.Attribute("x") ?? 100;
                double lly = (double?)videoElem.Attribute("y") ?? 500;
                double urx = llx + ((double?)videoElem.Attribute("width") ?? 200);
                double ury = lly + ((double?)videoElem.Attribute("height") ?? 150);
                string videoPath = videoElem.Value.Trim();

                if (!File.Exists(videoPath))
                {
                    Console.Error.WriteLine($"Video file not found: {videoPath}");
                    continue; // skip this entry
                }

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for video {videoPath}");
                    continue;
                }

                // Create a rectangle that defines the annotation's location on the page.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Use ScreenAnnotation which directly supports embedding a video file.
                // Title is defined on MarkupAnnotation, which ScreenAnnotation inherits from.
                var screen = new ScreenAnnotation(pdfDoc.Pages[pageNumber], rect, videoPath)
                {
                    Title = Path.GetFileName(videoPath)
                };

                // Add the annotation to the page.
                pdfDoc.Pages[pageNumber].Annotations.Add(screen);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded videos saved to '{outputPdf}'.");
    }
}
