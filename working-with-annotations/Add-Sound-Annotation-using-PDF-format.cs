using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output_with_sound.pdf"; // result PDF
        const string soundFilePath = "sound.wav";          // sound to embed

        // Verify required files exist
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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdfPath))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a SoundAnnotation (constructor: Page, Rectangle, sound file path)
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFilePath)
            {
                // Optional visual and textual properties
                Icon     = SoundIcon.Speaker,
                Title    = "Audio Note",
                Contents = "Click to play the embedded sound."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdfPath}'.");
    }
}