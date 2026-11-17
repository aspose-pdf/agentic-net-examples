using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_sound.pdf";
        const string soundFile = "background.wav"; // WAV/AIFF is required for SoundAnnotation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Select the page where the annotation will be placed (first page)
                Page page = doc.Pages[1];

                // Define the annotation rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create a SoundAnnotation. The constructor that includes the sound file path is required.
                // The annotation is played on click; automatic playback on page visibility is not supported.
                SoundAnnotation soundAnno = new SoundAnnotation(page, rect, soundFile)
                {
                    Icon = SoundIcon.Speaker,          // Choose an icon to display
                    Contents = "Background music",   // Tooltip text
                    Color = Color.LightGray           // Border colour (optional)
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(soundAnno);

                // Save the modified PDF (still inside the using block)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with sound annotation: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
