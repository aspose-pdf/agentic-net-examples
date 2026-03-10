using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, optional image watermark, optional text watermark
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "watermarked_output.pdf";
        const string imagePath = "watermark.png";   // set to null or empty if not used
        const string watermarkText = "CONFIDENTIAL"; // set to null or empty if not used

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // ---------- Image watermark (using WatermarkArtifact) ----------
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                foreach (Page page in doc.Pages)
                {
                    // Create a watermark artifact for the current page
                    WatermarkArtifact artifact = new WatermarkArtifact();
                    artifact.SetImage(imagePath);
                    artifact.Opacity = 0.3;               // 30 % opacity
                    artifact.IsBackground = false;        // overlay (true would place it behind)
                    artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                    artifact.ArtifactVerticalAlignment = VerticalAlignment.Center;
                    page.Artifacts.Add(artifact);
                }
            }

            // ---------- Text watermark (using TextStamp) ----------
            if (!string.IsNullOrEmpty(watermarkText))
            {
                // Configure the text stamp
                TextStamp txtStamp = new TextStamp(watermarkText)
                {
                    Opacity = 0.3,                                 // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    RotateAngle = 45,                               // diagonal watermark
                    Background = true                               // place behind existing content
                };

                // Set font‑related properties via the existing TextState instance
                txtStamp.TextState.Font = FontRepository.FindFont("Arial");
                txtStamp.TextState.FontSize = 72;
                txtStamp.TextState.FontStyle = FontStyles.Bold;
                txtStamp.TextState.ForegroundColor = Color.Gray;

                // Apply the stamp to every page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(txtStamp);
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
