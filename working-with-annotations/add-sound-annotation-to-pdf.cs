using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string mp3File   = "sound.mp3";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mp3File))
        {
            Console.Error.WriteLine($"MP3 file not found: {mp3File}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Select the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create the SoundAnnotation with the MP3 file
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, mp3File)
            {
                // Optional visual settings
                Icon     = SoundIcon.Speaker,          // display a speaker icon
                Title    = "Play Audio",               // tooltip title
                Contents = "Click to play the attached sound."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Sound annotation added and saved to '{outputPdf}'.");
    }
}