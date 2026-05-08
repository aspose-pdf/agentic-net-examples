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
        const string audioFolder = "AudioFiles"; // folder containing audio files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!Directory.Exists(audioFolder))
        {
            Console.Error.WriteLine($"Audio folder not found: {audioFolder}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Build the expected audio file name for this page.
                // Example: audio1.wav, audio2.wav, ... placed in the AudioFiles folder.
                string audioPath = Path.Combine(audioFolder, $"audio{i}.wav");

                if (!File.Exists(audioPath))
                {
                    Console.Error.WriteLine($"Audio file missing for page {i}: {audioPath}");
                    continue; // skip this page but continue processing others
                }

                // Define a rectangle where the sound annotation icon will appear.
                // Adjust coordinates as needed (left, bottom, right, top).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Create the SoundAnnotation using the constructor (SoundAnnotation(Page, Rectangle, string))
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioPath)
                {
                    // Optional: choose an icon (Speaker or Mic)
                    Icon = SoundIcon.Speaker,
                    // Optional: set a tooltip or description
                    Title = $"Audio for page {i}",
                    Contents = $"Plays audio file: audio{i}.wav"
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPdf}'.");
    }
}