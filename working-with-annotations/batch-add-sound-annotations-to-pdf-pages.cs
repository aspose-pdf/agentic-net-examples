using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_sound.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Assume audio files are named audio1.wav, audio2.wav, ... in the same directory as the executable
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Build the audio file name for the current page
                string audioFile = $"audio{i}.wav";

                if (!File.Exists(audioFile))
                {
                    Console.Error.WriteLine($"Audio file not found for page {i}: {audioFile}");
                    continue; // Skip this page if the audio file is missing
                }

                // Create the sound annotation and add it to the page
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioFile);
                soundAnn.Icon = SoundIcon.Speaker; // Optional: set the icon displayed on the page
                page.Annotations.Add(soundAnn);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with sound annotations to '{outputPath}'.");
    }
}