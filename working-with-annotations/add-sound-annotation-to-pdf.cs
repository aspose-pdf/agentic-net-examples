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
        // Aspose.Pdf supports only WAV/AIFF for sound annotations. Use a WAV file.
        const string wavFile   = "sound.wav";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(wavFile))
        {
            Console.Error.WriteLine($"WAV file not found: {wavFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 200, 550);

            // Create the sound annotation – constructor expects the sound file path (WAV/AIFF).
            SoundAnnotation soundAnnot = new SoundAnnotation(page, rect, wavFile)
            {
                // Choose an icon that will be shown on the page.
                Icon = SoundIcon.Speaker,
                // Optional tooltip text.
                Contents = "Play sound"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdf}'.");
    }
}
