using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the sound file (must be a supported audio format, e.g., .wav)
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_sound.pdf";
        const string soundFilePath = "chime.wav";

        // The page number on which the sound should be triggered when it becomes visible.
        // Aspose.Pdf uses 1‑based page indexing.
        const int targetPageNumber = 3;

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(soundFilePath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFilePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in a using block)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[targetPageNumber];

            // Define the rectangle where the annotation will be placed.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a SoundAnnotation that references the sound file.
            // The annotation will play the sound when the user activates it.
            // (Activation on page visibility is the default behavior for sound annotations in many viewers.)
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFilePath)
            {
                // Choose an icon to display (Speaker or Mic)
                Icon = SoundIcon.Speaker,
                // Optional tooltip text shown on hover
                Contents = "Page chime"
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(soundAnn);

            // Save the modified PDF (lifecycle rule: use Document.Save inside the using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with sound annotation: {outputPdfPath}");
    }
}