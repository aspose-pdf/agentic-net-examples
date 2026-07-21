using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_sound_annotations.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Prepare an array of audio files – one per page.
        // Adjust the paths/names as needed for your environment.
        string[] audioFiles = new string[]
        {
            "audio_page1.wav",
            "audio_page2.wav",
            "audio_page3.wav",
            // Add more entries if the PDF has more pages
        };

        try
        {
            // Load the existing PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                int pageCount = pdfDoc.Pages.Count; // 1‑based indexing (global rule)

                // Ensure we have an audio file for each page
                if (audioFiles.Length < pageCount)
                {
                    Console.Error.WriteLine("Not enough audio files supplied for the number of pages.");
                    return;
                }

                // Iterate over each page and add a SoundAnnotation
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // Define the annotation rectangle (left, bottom, right, top)
                    // Adjust coordinates as needed.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 120, 120);

                    // Create the SoundAnnotation using the constructor (page, rect, soundFile)
                    SoundAnnotation soundAnn = new SoundAnnotation(page, rect, audioFiles[i - 1]);

                    // Optional: set an icon (Speaker or Mic) – default is Speaker
                    // soundAnn.Icon = SoundIcon.Mic;

                    // Add the annotation to the page's annotation collection
                    page.Annotations.Add(soundAnn);
                }

                // Save the modified PDF (lifecycle rule: use Document.Save)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF with sound annotations saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}