using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input OFD file and multimedia file paths
        const string ofdPath   = "input.ofd";
        const string mediaPath = "sample_video.mp4";   // any supported video/audio file
        const string pdfPath   = "output.pdf";

        // Verify files exist
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load OFD document (OFD is input‑only, we convert to PDF)
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define annotation rectangle (left, bottom, right, top)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a Screen annotation that embeds the multimedia file.
            // Note: In recent versions of Aspose.Pdf the "Activation" property was removed.
            // The default activation (PageOpen) is sufficient for most scenarios.
            ScreenAnnotation mediaAnn = new ScreenAnnotation(page, rect, mediaPath)
            {
                // Optional visual settings
                Color = Aspose.Pdf.Color.LightGray,
                Contents = "Multimedia annotation (video/audio)"
                // Activation property omitted – not available in current library version
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(mediaAnn);

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Multimedia PDF saved to '{pdfPath}'.");
    }
}
