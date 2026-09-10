using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_screen_annotation.pdf";
        const string videoFile = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle (llx, lly, urx, ury) where the video will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a ScreenAnnotation on the first page that points to the video file
            ScreenAnnotation screen = new ScreenAnnotation(doc.Pages[1], rect, videoFile);

            // Disable user interaction by making the annotation read‑only
            screen.Flags = AnnotationFlags.ReadOnly;

            // Loop playback – Aspose.Pdf does not expose a direct property.
            // If the viewer supports it, a custom entry (e.g., /Loop) can be added to the annotation dictionary.
            // This placeholder shows where such a setting would be applied.
            // screen.AdditionalActions["Loop"] = true; // Uncomment and adjust if supported by the API

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(screen);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with screen annotation: {outputPdf}");
    }
}