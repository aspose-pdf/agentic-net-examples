using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the input CGM file, the sound file, and the output PDF.
        const string cgmPath   = "input.cgm";
        const string soundPath = "sound.wav";
        const string pdfPath   = "output.pdf";

        // Verify that required files exist.
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }
        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the CGM file into a PDF document using the appropriate load options.
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page.
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Choose the first page to place the sound annotation.
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear.
            // (left, bottom, width, height) – adjust as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

            // Create the sound annotation, referencing the external sound file.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundPath)
            {
                // Choose an icon to represent the annotation (Speaker or Mic).
                Icon = SoundIcon.Speaker,
                // Optional: set a title and contents for the popup.
                Title = "Audio Note",
                Contents = "Click to play the attached sound."
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(soundAnn);

            // Save the resulting PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{pdfPath}'.");
    }
}