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
        const string soundFile = "narration.wav";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Open the PDF in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the document has at least five pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Retrieve page five
            Page page = doc.Pages[5];

            // Define the clickable region (lower‑left x,y and upper‑right x,y)
            Rectangle rect = new Rectangle(100, 500, 300, 550);

            // Create the sound annotation on the specified region, providing the sound file path
            SoundAnnotation soundAnnot = new SoundAnnotation(page, rect, soundFile)
            {
                // Optional: set an icon that will be displayed on the page
                Icon = SoundIcon.Speaker,
                // Optional: tooltip text shown when hovering
                Contents = "Play narration"
            };

            // Add the annotation to the page
            page.Annotations.Add(soundAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdf}'.");
    }
}
