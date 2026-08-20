using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for FontRepository and TextState

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

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Build watermark text that includes the current year
            string watermarkText = $"© {DateTime.Now.Year} Confidential";

            // Create a TextStamp with the watermark text
            TextStamp stamp = new TextStamp(watermarkText);

            // Configure visual appearance of the stamp
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 48;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            stamp.Opacity = 0.3f;                         // semi‑transparent
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;
            stamp.RotateAngle = 45;                       // optional rotation

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}