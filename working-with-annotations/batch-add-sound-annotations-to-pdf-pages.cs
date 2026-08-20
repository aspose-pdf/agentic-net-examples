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

        // Audio files – one per page (adjust the list to match your PDF).
        string[] audioFiles = { "page1.wav", "page2.wav", "page3.wav" };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the audio files exist.
        foreach (var audio in audioFiles)
        {
            if (!File.Exists(audio))
            {
                Console.Error.WriteLine($"Audio file not found: {audio}");
                return;
            }
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Select the audio file for the current page.
                string soundFile = i <= audioFiles.Length ? audioFiles[i - 1] : audioFiles[audioFiles.Length - 1];

                // Define the annotation rectangle (left, bottom, right, top).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Create the sound annotation.
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
                {
                    Icon = SoundIcon.Speaker,               // Choose an icon.
                    Title = $"Audio for page {i}",           // Tooltip title.
                    Contents = $"Play audio: {Path.GetFileName(soundFile)}"
                };

                // Add the annotation to the page.
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotations: {outputPath}");
    }
}