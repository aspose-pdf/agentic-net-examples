using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_sound.pdf"; // result PDF
        const string mp3Path   = "sound.mp3";          // MP3 file to play

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(mp3Path))
        {
            Console.Error.WriteLine($"MP3 file not found: {mp3Path}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the SoundAnnotation. The constructor takes the page,
            // the rectangle, and the path to the sound file.
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, mp3Path)
            {
                // Optional: set an icon that will be displayed on the page
                Icon = SoundIcon.Speaker,
                // Optional: provide a tooltip / title for the annotation
                Title = "Play Sound",
                // Optional: description shown in the popup
                Contents = "Click to play the attached MP3."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Sound annotation added and saved to '{outputPdf}'.");
    }
}