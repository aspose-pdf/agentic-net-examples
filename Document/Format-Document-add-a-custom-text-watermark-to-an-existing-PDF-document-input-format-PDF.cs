using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a text stamp that will serve as the watermark
        TextStamp watermark = new TextStamp("CONFIDENTIAL")
        {
            // Place the stamp in the center of each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,

            // Make the watermark semi‑transparent
            Opacity = 0.5,

            // Rotate the text for a typical diagonal watermark
            RotateAngle = 45,

            // Ensure the stamp is drawn over the page content
            Background = false
        };

        // Configure the visual appearance of the text
        watermark.TextState.Font = FontRepository.FindFont("Arial");
        watermark.TextState.FontSize = 72;
        watermark.TextState.ForegroundColor = Color.Gray;

        // Apply the watermark to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(watermark);
        }

        // Save the modified PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);
    }
}