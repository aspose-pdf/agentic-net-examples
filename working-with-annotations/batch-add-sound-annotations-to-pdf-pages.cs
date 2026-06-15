using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_sounds.pdf";

        // Directory that contains the audio files (one per page)
        const string audioDir = "AudioFiles";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify audio directory exists
        if (!Directory.Exists(audioDir))
        {
            Console.Error.WriteLine($"Audio directory not found: {audioDir}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Build expected audio file name for this page (e.g., "page1.wav")
                string audioFile = Path.Combine(audioDir, $"page{i}.wav");

                // Skip if the audio file does not exist for this page
                if (!File.Exists(audioFile))
                {
                    Console.WriteLine($"Audio file not found for page {i}: {audioFile} – skipping annotation.");
                    continue;
                }

                // Define the annotation rectangle (position and size on the page)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Create the SoundAnnotation (constructor rule)
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioFile)
                {
                    // Optional: set an icon (Speaker or Mic) and a tooltip
                    Icon = SoundIcon.Speaker,
                    Title = $"Audio for page {i}",
                    Contents = $"Play audio file: {Path.GetFileName(audioFile)}"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPdf}'.");
    }
}