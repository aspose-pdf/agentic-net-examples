using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for Font, FontRepository, FontStyles, TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages and add a TextStamp watermark
                foreach (Page page in doc.Pages)
                {
                    // Create a new TextStamp for each page
                    TextStamp stamp = new TextStamp(watermarkText);

                    // Position and appearance
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Center;
                    stamp.RotateAngle         = 45;               // diagonal
                    stamp.Opacity             = 0.3;              // semi‑transparent
                    stamp.Background          = false;            // no background box

                    // Configure the TextState (read‑only property – set its members)
                    stamp.TextState.Font      = FontRepository.FindFont("Arial");
                    stamp.TextState.FontSize  = 72;
                    stamp.TextState.FontStyle = FontStyles.Bold;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                    // Apply the stamp to the current page
                    page.AddStamp(stamp);
                }

                // Save the watermarked PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}