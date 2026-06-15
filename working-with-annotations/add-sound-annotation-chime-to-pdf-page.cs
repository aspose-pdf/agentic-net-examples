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
        const int    targetPage = 3;                    // page where the sound should be triggered

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[targetPage];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the SoundAnnotation – it will play the specified sound when activated
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile)
            {
                // Optional: choose an icon (Speaker or Mic)
                Icon = SoundIcon.Speaker,

                // Optional: tooltip text shown on hover
                Contents = "Page chime"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with SoundAnnotation on page {targetPage}: {outputPdf}");
    }
}