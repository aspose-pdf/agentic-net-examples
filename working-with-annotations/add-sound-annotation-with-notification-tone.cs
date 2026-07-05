using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // SoundAnnotation resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_sound.pdf";
        const string soundPath  = "notification.wav";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a SoundAnnotation that references the sound file
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath);

            // Set a visible icon (speaker) so the annotation appears on the page
            soundAnn.Icon = SoundIcon.Speaker;

            // Optional tooltip text shown on hover
            soundAnn.Contents = "Click to play notification tone";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with SoundAnnotation saved to '{outputPath}'.");
    }
}