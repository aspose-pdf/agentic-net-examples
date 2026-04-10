using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        // Input PDF bytes (replace with actual PDF data)
        byte[] pdfBytes = File.ReadAllBytes("input.pdf"); // example source

        // Output PDF path
        const string outputPath = "watermarked.pdf";

        // Multi‑line watermark text
        string watermarkText = "Confidential\r\nDo Not Distribute";

        // Create a memory stream for the input PDF
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Initialize the PdfFileStamp facade
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                // Bind the PDF stream to the facade
                fileStamp.BindPdf(inputStream);

                // Create formatted text for the watermark
                // Parameters: text, color, font name, encoding, embed flag, font size
                FormattedText ft = new FormattedText(
                    watermarkText,
                    System.Drawing.Color.FromArgb(128, 255, 0, 0), // semi‑transparent red
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    48f); // custom font size

                // Add the formatted text as a header on each page.
                // Using a large top margin places the text near the top; adjust as needed.
                // The same call adds the text to all pages.
                fileStamp.AddHeader(ft, 0f, 0f, 0f);

                // Save the result to a file (or to another stream if required)
                fileStamp.Save(outputPath);
                fileStamp.Close(); // optional; using will dispose automatically
            }
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}