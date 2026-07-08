using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_with_sound.pdf"; // result PDF
        const string soundFile  = "chime.wav";          // sound to play
        const int    targetPage = 3;                    // page where the sound triggers

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPage];

            // Define a small rectangle where the annotation icon will appear.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the SoundAnnotation.
            // Constructor: SoundAnnotation(Page, Rectangle, string soundFilePath)
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Choose an icon that represents a speaker.
                Icon = SoundIcon.Speaker,

                // Optional: give the annotation a title and contents.
                Title    = "Page Chime",
                Contents = "A chime will play when this page becomes visible."
            };

            // Add the annotation to the page's annotation collection.
            // The overload without the rotation flag is sufficient here.
            page.Annotations.Add(soundAnn);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with SoundAnnotation: {outputPdf}");
    }
}