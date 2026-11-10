using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "filled_form.pdf";   // PDF after AutoFiller has populated the form
        const string outputPath = "watermarked.pdf";   // Resulting PDF with watermark
        const string watermark  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF that already contains filled form fields
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Create a TextStamp that will be used as a watermark on every page
            // -----------------------------------------------------------------
            TextStamp stamp = new TextStamp(watermark);

            // Configure the visual appearance of the stamp
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.8, 0.8); // light gray
            stamp.Opacity = 0.3;                     // semi‑transparent
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;
            stamp.RotateAngle = 45f;                 // rotate 45 degrees around the stamp centre

            // -----------------------------------------------------------------
            // Apply the stamp to each page of the document
            // -----------------------------------------------------------------
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the watermarked PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}