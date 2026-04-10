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
        Document pdfDoc = new Document(inputPath);

        // Create a text stamp with the required appearance
        TextStamp stamp = new TextStamp("Confidential");
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // red font
        // semi‑transparent red background (alpha 128 out of 255 ≈ 50% opacity)
        stamp.TextState.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);
        stamp.Opacity = 0.5f; // overall stamp opacity (optional)
        // Position the stamp – centered on each page
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Center;

        // Apply the stamp to every page
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the result
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
