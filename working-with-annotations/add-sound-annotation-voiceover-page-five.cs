using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // allowed, may be needed for other operations

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_sound.pdf";
        const string soundFile = "voiceover.wav";

        // Verify input files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(soundFile))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFile}");
            return;
        }

        // Open the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Get page five
            Aspose.Pdf.Page page = doc.Pages[5];

            // Define the clickable region (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SoundAnnotation that plays the specified audio file
            Aspose.Pdf.Annotations.SoundAnnotation soundAnnot =
                new Aspose.Pdf.Annotations.SoundAnnotation(page, rect, soundFile);

            // Optional: give the annotation a name
            soundAnnot.Name = "VoiceOver";

            // Add the annotation to the page
            page.Annotations.Add(soundAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotation to '{outputPath}'.");
    }
}