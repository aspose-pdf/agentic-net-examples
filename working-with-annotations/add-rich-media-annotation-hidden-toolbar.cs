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
        const string videoPath = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the source PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Embed the video content
            using (FileStream videoStream = File.OpenRead(videoPath))
            {
                richMedia.SetContent(Path.GetFileName(videoPath), videoStream);
            }

            // Hide the playback toolbar via custom flash variables (e.g., toolbar=false)
            // The CustomFlashVariables property expects a string, not a stream.
            string flashVars = "toolbar=false";
            richMedia.CustomFlashVariables = flashVars;

            // Activate the annotation on click (default, but set explicitly for clarity)
            richMedia.ActivateOn = RichMediaAnnotation.ActivationEvent.Click;

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPdf}'.");
    }
}
