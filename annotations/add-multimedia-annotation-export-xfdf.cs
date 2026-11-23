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
        const string mediaFile = "sample.mp4"; // path to multimedia file (audio/video)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Multimedia file not found: {mediaFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a screen (video) annotation to the first page
            // -------------------------------------------------
            // Define the annotation rectangle (llx, lly, urx, ury)
            var rect = new Rectangle(100, 500, 300, 600);

            // ScreenAnnotation is the core API class for embedding video/audio.
            var screenAnnotation = new ScreenAnnotation(doc.Pages[1], rect, mediaFile)
            {
                Title = "Sample Video"
            };

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(screenAnnotation);

            // -------------------------------------------------
            // 2. Export all annotations to XFDF via a MemoryStream
            // -------------------------------------------------
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export annotations in XFDF format to the stream
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position if you need to read the XFDF content later
                xfdfStream.Position = 0;

                // Example: write XFDF to a file (optional)
                // File.WriteAllBytes("annotations.xfdf", xfdfStream.ToArray());
            }

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with video annotation added.");
    }
}
