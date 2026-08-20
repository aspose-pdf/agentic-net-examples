using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoUrl  = "https://example.com/video.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the screen annotation will appear
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a ScreenAnnotation.
            // The third parameter is a placeholder for a local media file; we pass an empty string
            // because we will use an external URL via an action.
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, string.Empty);

            // Add an action that opens the external video URL.
            // The Actions collection allows us to attach a GoToURIAction.
            screen.Actions.Add(new GoToURIAction(videoUrl));

            // Optionally set the annotation to be visible and active on page open.
            // Setting the ActiveState to "Play" is not required for external URLs,
            // but we can set the annotation's appearance state if needed.
            // screen.ActiveState = "Play"; // Uncomment if a specific state is required.

            // Add the annotation to the page.
            page.Annotations.Add(screen);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation with video URL added and saved to '{outputPdf}'.");
    }
}