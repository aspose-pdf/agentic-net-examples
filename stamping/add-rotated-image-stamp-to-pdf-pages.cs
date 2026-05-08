using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "stamp.png"; // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp – this stamp will be placed on each page
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                // Position the stamp (example: centered)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Rotate the stamp 90 degrees clockwise to match portrait‑to‑landscape orientation
                // NOTE: Aspose.Pdf.Rotation enum uses the "on" prefix (on90, on180, ...)
                Rotate = Rotation.on90,

                // Optional visual settings
                Opacity = 0.5f,
                Background = false
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
