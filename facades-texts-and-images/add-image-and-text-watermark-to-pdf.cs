using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "watermarked.pdf";
        const string imagePath     = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Load the source PDF
        Document pdf = new Document(inputPdf);

        // ---------------------------------------------------------------
        // 1. Image watermark (logo) – placed on page 1
        // ---------------------------------------------------------------
        // Use the core ImageStamp (not the Facades.Stamp) – it exposes Width, Height,
        // XIndent, YIndent and Opacity directly.
        var imgStamp = new ImageStamp(imagePath)
        {
            Width   = 200f,   // Desired width (points)
            Height  = 200f,   // Desired height (points)
            XIndent = 100f,   // X position from left (points)
            YIndent = 400f,   // Y position from bottom (points)
            Opacity = 0.5f,   // Semi‑transparent image
            Background = false // Draw over existing content
        };
        pdf.Pages[1].AddStamp(imgStamp);

        // ---------------------------------------------------------------
        // 2. Text watermark – semi‑transparent, rotated, centered
        // ---------------------------------------------------------------
        TextStamp txtStamp = new TextStamp(watermarkText)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            Background          = false,
            RotateAngle         = 45f,
            Opacity             = 0.5f,
            // The stamp will cover the whole page; alignment properties centre the text.
            XIndent = 0,
            YIndent = 0,
            Width   = pdf.Pages[1].PageInfo.Width,
            Height  = pdf.Pages[1].PageInfo.Height
        };
        // Font and style settings
        txtStamp.TextState.Font      = FontRepository.FindFont("Arial");
        txtStamp.TextState.FontSize  = 72;
        txtStamp.TextState.FontStyle = FontStyles.Bold;
        txtStamp.TextState.ForegroundColor = Color.Red;

        pdf.Pages[1].AddStamp(txtStamp);

        // ---------------------------------------------------------------
        // 3. Save the modified PDF
        // ---------------------------------------------------------------
        pdf.Save(outputPdf);
        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
