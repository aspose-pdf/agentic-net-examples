using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string soundPath  = "notification.wav";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a small rectangle for the annotation (position can be adjusted)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 20, 20);

                // Create a SoundAnnotation that plays the specified sound file
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath);

                // Use the speaker icon; the annotation will be activated when the page is opened
                soundAnn.Icon = SoundIcon.Speaker;

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sound annotations saved to '{outputPath}'.");
    }
}