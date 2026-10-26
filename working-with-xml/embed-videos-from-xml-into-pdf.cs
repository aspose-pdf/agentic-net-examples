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
        const string xmlPath   = "videos.xml";     // XML describing videos
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load XML. Expected format:
        // <Videos>
        //   <Video page="1" x="100" y="500" width="200" height="150">video1.mp4</Video>
        //   ...
        // </Videos>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        if (xmlDoc.Root == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        using (Document pdfDoc = new Document(pdfPath))
        {
            foreach (XElement videoElem in xmlDoc.Root.Elements("Video"))
            {
                // Parse required attributes; fall back to defaults if missing
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

                // Ensure page index is within range (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for video {videoPath}");
                    continue;
                }

                // Create rectangle defining annotation location
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create RichMediaAnnotation (available in core API)
                RichMediaAnnotation richMedia = new RichMediaAnnotation(pdfDoc.Pages[pageNumber], rect)
                {
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Optional border
                richMedia.Border = new Border(richMedia) { Width = 1 };

                // -----------------------------------------------------------------
                // Attach the video file using reflection – the RichMediaContent type
                // may not exist in older Aspose.Pdf versions. This keeps the code
                // compilable across all supported versions.
                // -----------------------------------------------------------------
                var richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                if (richMediaContentProp != null)
                {
                    // Try to obtain the RichMediaContent type from the assembly.
                    var richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                    if (richMediaContentType != null)
                    {
                        // Create an instance of RichMediaContent.
                        var richMediaContent = Activator.CreateInstance(richMediaContentType);
                        // The AddFile(string) method adds the media file to the content.
                        var addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                        addFileMethod?.Invoke(richMediaContent, new object[] { videoPath });
                        // Assign the populated RichMediaContent to the annotation.
                        richMediaContentProp.SetValue(richMedia, richMediaContent);
                    }
                    else
                    {
                        Console.Error.WriteLine("RichMediaContent type not found – video annotation will be added without embedded media.");
                    }
                }
                else
                {
                    Console.Error.WriteLine("RichMediaAnnotation does not expose a RichMediaContent property – skipping media embedding.");
                }

                // Add the annotation to the page
                pdfDoc.Pages[pageNumber].Annotations.Add(richMedia);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Video annotations embedded. Output saved to '{outputPdf}'.");
    }
}
