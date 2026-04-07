using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_sound.pdf";
        const string audioFolder = "Audio"; // Folder containing audio files named audio1.wav, audio2.wav, ...

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Build the path to the audio file for this page
                string audioPath = Path.Combine(audioFolder, $"audio{i}.wav");
                if (!File.Exists(audioPath))
                {
                    Console.Error.WriteLine($"Audio file missing for page {i}: {audioPath}");
                    continue; // Skip annotation if audio is unavailable
                }

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 150, 150);

                // Create a SoundAnnotation using the page, rectangle, and audio file
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioPath)
                {
                    Icon = SoundIcon.Speaker,               // Choose an icon (Speaker or Mic)
                    Title = $"Audio for page {i}",           // Optional title shown in the popup
                    Contents = $"Play audio{i}.wav"          // Optional description
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with sound annotations: {outputPdf}");
    }
}