using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF using the high‑level Document API (no PdfContentEditor needed)
        Document pdf = new Document(inputPdf);

        // ---------------------------------------------------------------------
        // 1️⃣ Add the image as a background stamp on page 1
        // ---------------------------------------------------------------------
        ImageStamp imgStamp = new ImageStamp(imagePath)
        {
            // Position the stamp – use XIndent/YIndent instead of non‑existent X/Y
            XIndent = 100,
            YIndent = 500,
            // Size of the stamp
            Width = 300,   // 400 - 100
            Height = 300,  // 800 - 500
            Background = true   // place behind existing page content
        };
        pdf.Pages[1].AddStamp(imgStamp);

        // ---------------------------------------------------------------------
        // 2️⃣ Add semi‑transparent text over the image on the same page
        // ---------------------------------------------------------------------
        TextStamp txtStamp = new TextStamp(watermarkText)
        {
            XIndent = 100,
            YIndent = 500,
            Width = 300,
            Height = 300,
            // Opacity controls the overall stamp transparency (0 = fully transparent, 1 = opaque)
            Opacity = 0.5f
        };
        // Configure the visual appearance of the text
        txtStamp.TextState.FontSize = 48;
        txtStamp.TextState.FontStyle = FontStyles.Bold;
        txtStamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // red
        // No ForegroundOpacity property – opacity is handled by the stamp itself

        pdf.Pages[1].AddStamp(txtStamp);

        // Save the modified PDF
        pdf.Save(outputPdf);

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
