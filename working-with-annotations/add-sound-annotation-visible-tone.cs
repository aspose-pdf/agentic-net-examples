using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_sound.pdf";
        const string soundPath = "tone.wav";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(soundPath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the annotation
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X,Y and upper‑right X,Y in points)
            Rectangle rect = new Rectangle(100, 500, 200, 600);

            // Create a SoundAnnotation and associate the audio file directly via the constructor
            // This constructor is available in the core Aspose.Pdf API and does not require the deprecated "Sound" property.
            SoundAnnotation soundAnnot = new SoundAnnotation(page, rect, soundPath);

            // Optional: set an icon if the enum is present in the used library version.
            // Uncomment the line below when SoundAnnotationIcon is available.
            // soundAnnot.Icon = SoundAnnotationIcon.Speaker;

            // The annotation is visible by default (no Hidden/NoView flags are set).
            page.Annotations.Add(soundAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with sound annotation: {outputPath}");
    }
}
