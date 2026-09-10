using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Build the multi‑line disclaimer text
            string disclaimerText = "This document is confidential.\nPlease do not distribute.\n© Company";

            // Create a text stamp
            TextStamp stamp = new TextStamp(disclaimerText)
            {
                // Position the stamp at the bottom centre of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Make the stamp semi‑transparent
                Opacity = 0.5f,
                // Draw the stamp as text (not as graphic operators)
                Draw = false
            };

            // Configure text appearance – TextState is read‑only, so modify the existing instance
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 12;
            stamp.TextState.ForegroundColor = Color.Black;

            // Custom line spacing (leading). The Leading property exists in newer versions of Aspose.PDF.
            // If your version does not expose it, simply omit this line – the default leading will be used.
            // stamp.TextState.Leading = 5f; // Uncomment if the property is available.

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
