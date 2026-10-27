using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        // Ensure there is a PDF to work with. If "input.pdf" does not exist, create a minimal one in memory.
        byte[] pdfBytes;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a placeholder PDF (one blank page) and keep it in a byte array.
            using var placeholderDoc = new Document();
            placeholderDoc.Pages.Add();
            using var ms = new MemoryStream();
            placeholderDoc.Save(ms);
            pdfBytes = ms.ToArray();
        }

        // Load the PDF document directly from the memory stream.
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Reset position to the beginning just in case.
            inputStream.Position = 0;
            Document pdfDoc = new Document(inputStream);

            // Multi‑line watermark text.
            string watermarkText = "Confidential\nDo Not Distribute";

            // Custom font size and color for the watermark.
            int fontSize = 48;
            Color watermarkColor = Color.FromRgb(1.0, 0.0, 0.0); // Red

            // Add the watermark to every page.
            foreach (Page page in pdfDoc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.SetTextAndState(watermarkText, new TextState
                {
                    FontSize = fontSize,
                    ForegroundColor = watermarkColor,
                    // Choose a font that exists on the system or embed one.
                    Font = FontRepository.FindFont("Arial")
                });
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;
                watermark.Opacity = 0.5;               // optional transparency
                watermark.IsBackground = true;        // render behind page content

                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF back to a memory stream (or directly to a file).
            using (MemoryStream resultStream = new MemoryStream())
            {
                pdfDoc.Save(resultStream);
                File.WriteAllBytes("watermarked_output.pdf", resultStream.ToArray());
                Console.WriteLine("Watermark added and saved to 'watermarked_output.pdf'.");
            }
        }
    }
}
