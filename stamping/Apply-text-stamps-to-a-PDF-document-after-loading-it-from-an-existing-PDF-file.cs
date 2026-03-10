using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired value
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Position the stamp (example: bottom‑right corner)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Margins from the page edges
                RightMargin  = 20,
                BottomMargin = 20,
                // Make the stamp semi‑transparent
                Opacity = 0.5
            };

            // Configure the stamp's appearance via the existing TextState instance
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.ForegroundColor = Color.Red;

            // Apply the stamp to every page in the document
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                page.AddStamp(textStamp);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
