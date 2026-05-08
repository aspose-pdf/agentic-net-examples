using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and MP3 file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_sound.pdf";
        const string soundFilePath = "audio.mp3";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(soundFilePath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFilePath}");
            return;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the SoundAnnotation.
            // Constructor: SoundAnnotation(Page, Rectangle, string soundFile)
            SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFilePath)
            {
                // Optional: set an icon (Speaker or Mic)
                Icon = SoundIcon.Speaker,
                // Optional: set a tooltip text
                Title = "Play Audio",
                // Optional: set a border color
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(soundAnn);

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdfPath}'.");
    }
}