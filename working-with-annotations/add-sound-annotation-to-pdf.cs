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
        const string soundFile = "sound.mp3";

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Select the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a SoundAnnotation that references the MP3 file
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Optional: choose an icon (Speaker or Mic)
                Icon = SoundIcon.Speaker,
                // Optional: set tooltip and description
                Title = "Play Audio",
                Contents = "Click to play the attached MP3"
            };

            // Add the annotation to the page
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdf}'.");
    }
}