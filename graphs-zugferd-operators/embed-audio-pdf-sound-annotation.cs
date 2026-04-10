using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // SoundAnnotation
using Aspose.Pdf;               // JavascriptAction

class Program
{
    static void Main()
    {
        const string outputPdf = "audio_embedded.pdf";
        const string audioFile = "sample.wav";   // Path to the audio file to embed

        if (!File.Exists(audioFile))
        {
            Console.Error.WriteLine($"Audio file not found: {audioFile}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the sound annotation will appear (coordinates in points)
            Aspose.Pdf.Rectangle soundRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the sound annotation using the audio file
            SoundAnnotation soundAnn = new SoundAnnotation(page, soundRect, audioFile);

            // Add the annotation to the page
            page.Annotations.Add(soundAnn);

            // Add JavaScript that plays the sound when the document is opened
            doc.OpenAction = new JavascriptAction("this.getAnnots()[0].play();");

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPdf}'.");
    }
}