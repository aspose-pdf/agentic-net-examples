using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_screen_annotation.pdf";
        const string videoUrl  = "https://example.com/video.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the screen annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation.
            // The third parameter is a placeholder for a media file path; we use an empty string
            // because the actual video will be referenced via an external URL action.
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, string.Empty);

            // Add an action that opens the external video URL.
            // Use the Actions collection to attach a GoToURIAction.
            screenAnn.Actions.Add(new GoToURIAction(videoUrl));

            // Set the annotation to start playing automatically when activated.
            // The ActiveState property accepts a string; "Play" triggers automatic playback.
            screenAnn.ActiveState = "Play";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPdf}");
    }
}