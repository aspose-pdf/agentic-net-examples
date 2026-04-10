using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextStamp with the custom message.
                TextStamp textStamp = new TextStamp(stampText);

                // Configure visual appearance of the stamp.
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;   // Center horizontally.
                textStamp.VerticalAlignment   = VerticalAlignment.Center;     // Center vertically.
                textStamp.Opacity             = 0.3f;                         // Semi‑transparent.
                textStamp.Background          = false;                        // Draw on top of page content.

                // Define text style (font, size, color) via the existing TextState instance.
                textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                textStamp.TextState.FontSize = 48;
                textStamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red color.

                // Add the stamp to the current page.
                page.AddStamp(textStamp);
            }

            // Save the modified PDF. No SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
    }
}
