using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_sound.pdf";
        const string soundFile = "voiceover.wav";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document has at least five pages (1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Define the annotation rectangle (llx, lly, urx, ury)
            // Fully qualify Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SoundAnnotation on page 5 using the constructor that takes page, rectangle, and sound file path
            SoundAnnotation soundAnn = new SoundAnnotation(doc.Pages[5], rect, soundFile);

            // Optional: set a tooltip or description
            soundAnn.Contents = "Play voice‑over narration";

            // Add the annotation to the page's annotation collection
            doc.Pages[5].Annotations.Add(soundAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdf}'.");
    }
}