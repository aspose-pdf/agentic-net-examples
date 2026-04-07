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
        const string soundFile = "notification.wav"; // must be a supported audio format

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the SoundAnnotation
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Use the speaker icon
                Icon = SoundIcon.Speaker,

                // Optional: set a tooltip or description
                Contents = "Notification tone (plays when annotation becomes visible)",
                Title    = "Alert"
            };

            // Add the annotation to the page
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with SoundAnnotation saved to '{outputPdf}'.");
    }
}