using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextState, FontRepository, FontStyles

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a semi‑transparent text stamp that will act as a watermark
            TextStamp stamp = new TextStamp("CONFIDENTIAL");
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;
            stamp.RotateAngle         = 45;      // diagonal watermark
            stamp.Opacity             = 0.3;     // 30% opacity (semi‑transparent)
            stamp.Background          = false;   // no opaque background rectangle

            // Configure the TextState of the stamp (TextState is read‑only, so modify its members directly)
            stamp.TextState.FontSize        = 72;
            stamp.TextState.FontStyle       = FontStyles.Bold;
            stamp.TextState.Font            = FontRepository.FindFont("Arial");
            stamp.TextState.ForegroundColor = Color.FromRgb(0.8, 0.0, 0.0); // light red

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
