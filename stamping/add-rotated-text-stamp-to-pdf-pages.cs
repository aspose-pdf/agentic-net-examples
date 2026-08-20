using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for TextStamp (inherits from Stamp)
using Aspose.Pdf.Text;       // for TextState if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: load via Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to each page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Position the stamp at the center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Rotate the stamp 90 degrees to match portrait‑to‑landscape orientation
                Rotate = Rotation.on90,

                // Optional visual styling
                Opacity = 0.5f,
                Background = false
            };

            // Apply the stamp to every page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF (using rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
