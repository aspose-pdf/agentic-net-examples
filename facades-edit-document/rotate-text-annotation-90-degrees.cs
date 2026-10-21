using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For TextStamp and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will act as the rotated annotation
            TextStamp txtStamp = new TextStamp("Diagonal Note");

            // -----------------------------------------------------------------
            // Positioning – you can adjust XIndent/YIndent or use alignment flags.
            // Here we place the stamp roughly at the centre of the first page.
            // -----------------------------------------------------------------
            txtStamp.HorizontalAlignment = HorizontalAlignment.Center;
            txtStamp.VerticalAlignment   = VerticalAlignment.Center;
            // Optional fine‑tuning offsets (in points)
            txtStamp.XIndent = 0;   // no extra horizontal offset
            txtStamp.YIndent = 0;   // no extra vertical offset

            // -----------------------------------------------------------------
            // Rotation – use RotateAngle (degrees) instead of the non‑existent
            // Rotation property.
            // -----------------------------------------------------------------
            txtStamp.RotateAngle = 90f; // rotates 90 degrees clockwise

            // -----------------------------------------------------------------
            // Visual appearance – TextState is read‑only, so we modify the
            // existing instance rather than assigning a new one.
            // -----------------------------------------------------------------
            txtStamp.TextState.FontSize   = 12;
            txtStamp.TextState.FontStyle  = FontStyles.Bold;
            txtStamp.TextState.Font       = FontRepository.FindFont("Arial");
            txtStamp.Opacity               = 0.5;   // semi‑transparent background
            txtStamp.Background            = true; // draw background rectangle

            // Add the stamp to the first page (or iterate over pages as needed)
            Page targetPage = doc.Pages[1];
            targetPage.AddStamp(txtStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text annotation saved to '{outputPath}'.");
    }
}
