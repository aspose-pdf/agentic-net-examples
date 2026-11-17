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
        const string audioFolder = "Audio"; // folder containing audio1.wav, audio2.wav, ...

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Build expected audio file name for this page
                string audioPath = Path.Combine(audioFolder, $"audio{pageIndex}.wav");
                if (!File.Exists(audioPath))
                {
                    Console.WriteLine($"Audio file not found for page {pageIndex}: {audioPath}. Skipping annotation.");
                    continue;
                }

                // Define a rectangle where the annotation icon will appear (lower‑left x,y, upper‑right x,y)
                // Here we place it near the bottom‑left corner of the page.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 50, 70, 70);

                // Create the sound annotation using the overload that accepts a file path.
                SoundAnnotation sound = new SoundAnnotation(page, rect, audioPath);
                sound.Title = $"Audio for page {pageIndex}";
                // Optional: set an icon (Speaker, Mic, etc.). Default is Speaker.
                // sound.Icon = SoundIcon.Speaker; // uncomment if needed

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(sound);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPdf}'.");
    }
}
