using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_sound.pdf"; // result PDF
        const string soundFile = "notification.wav";   // sound to play (must be WAV)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will appear (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation icon will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the SoundAnnotation with the sound file
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile);

            // Set an icon (Speaker or Mic) – here we use the speaker icon
            soundAnn.Icon = SoundIcon.Speaker;

            // Optional: set a title that appears in the popup window
            soundAnn.Title = "Notification";

            // Optional: set contents (tooltip text)
            soundAnn.Contents = "Click to hear the notification tone";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with SoundAnnotation saved to '{outputPdf}'.");
    }
}